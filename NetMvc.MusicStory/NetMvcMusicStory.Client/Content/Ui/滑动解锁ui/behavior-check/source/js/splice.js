class Splice {
    constructor({
        // 传入的dom容器表示。
        el, 
        // 传入的图片列表，是一个数组。
        imgs,          
        // 缺口吻合时的左右容差。
        dis = {
            left: 3,
            right: 3
        },
        /*
            缺口在canvas中绘制区域范围。
            这里的范围不是其绘制区域getContetx(2d)的范围，是相对该canvas在页面中所占位置的大小（px）。
        */
        range = {
            top: 5,
            right: 5,
            bottom: 5,
            left: 100
        },
        /*
            canvas绘制区域相对于页面所占位置区域的比率（由于移动端分辨率问题，同比率下绘制结果会失真）。
        */
        scale = 2,
        /*
            操作成功另外所需条件的函数。
            会接收2个参数，第一个是成功执行的操作函数，第二个是失败执行的操作函数，在条件满足时，手动调用即可。
        */
        condition,
        success,
        fail
    }) {
        this.container      = document.querySelector(el);
        this.splice         = this.createDiv(this.container, "bc-splice");
        this.screen         = this.createDiv(this.splice, "bc-splice__screen");
        this.strip          = this.createDiv(this.splice, "bc-splice__strip");
        this.stripH         = this.strip.offsetHeight;
        this.img            = this.createDiv(this.screen, "bc-splice__img");
        this.rip            = this.createCanvas(this.img, "bc-splice__canvas--rip", scale);
        this.ripArea        = this.rip.getContext("2d");
        this.piece          = this.createCanvas(this.img, "bc-splice__canvas--piece", scale);
        this.pieceArea      = this.piece.getContext("2d");
        this.readyPrompt    = this.createDiv(this.splice, "bc-splice__prompt--ready");
        this.startPrompt    = this.createDiv(this.splice, "bc-splice__prompt--start");
        this.slidePrompt    = this.createDiv(this.splice, "bc-splice__prompt--slide");
        this.loadPrompt     = this.createDiv(this.splice, "bc-splice__prompt--check");
        this.successPrompt  = this.createDiv(this.splice, "bc-splice__prompt--success");
        this.failPrompt     = this.createDiv(this.splice, "bc-splice__prompt--fail");
        this.updateBtn      = this.createDiv(this.img, "bc-splice__button--update");
        this.closeBtn       = this.createDiv(this.img, "bc-splice__button--close");
        this.slideBtn       = this.createDiv(this.strip, "bc-splice__button--slide");

        this.imgs = imgs;
        // new Image()加载成功后的图像。
        this.imgObjs = null;
        this.dis = dis;
        this.range = {
            top: range.top * scale,
            right: range.right * scale,
            bottom: range.bottom * scale,
            left: range.left * scale
        };
        this.scale = scale;
        // 缺口的宽高（宽高是一样的）。
        this.size = this.piece.width;
        /*
            存储随机图片的相关信息。
            obj：随机产生image对象；
            index：该对象在imgObjs列表中的下标；
            x/y：在canvas中绘制的x/y坐标；
            width/height：在canvas中绘制的宽高，这里与canvas（这里的canvas不是会滑动的canvas）的宽高比率对比，进而采取裁剪。
        */
        this.randomImgInfo = {
            obj: null,
            index: null,
            x: null,
            y: null,
            width: null,
            height: null
        };
        // 随机缺口在canvas（这里的canvas不是会滑动的canvas）绘制的x/y坐标。
        this.randomRipInfo = {
            x: null,
            y: null
        };
        this.condition = condition;
        this.success = success;
        this.fail = fail;
        // 判断是否在效验中，禁止过程其他事件的触发。
        this.checking = false;

        this.init();
    }
    // 对属性的操作和已存在DOM的操作都在init中执行，方便直接查看。
    init() {
        /*
            判断移动PC设备，切换touch事件。
            这里需要注意的是，由于将值赋值给了addEvent，因此通过addEvent调用的函数内this将不再指向Splice。
        */
        let addEvent = null;
        if (/phone|pad|pod|iphone|ipod|ios|ipad|android|mobile/.test(window.navigator.userAgent.toLowerCase())) {
            addEvent = this.createSlideXTouchEvent;
        } else {
            addEvent = this.createSlideXMouseEvent;
        }

        // 设置提示语内容垂直居中，为适应多样化的高度。
        this.readyPrompt.style.lineHeight = this.stripH + "px";
        this.startPrompt.style.lineHeight = this.stripH + "px";
        this.slidePrompt.style.lineHeight = this.stripH + "px";
        this.loadPrompt.style.lineHeight = this.stripH + "px";
        this.successPrompt.style.lineHeight = this.stripH + "px";
        this.failPrompt.style.lineHeight = this.stripH + "px";

        // 设置状态提示，通过控制class类名。
        const setSpliceState = (className = "") => {
            let classStr = this.splice.getAttribute("class");
            if (classStr) {
                classStr = classStr.replace(/\s*bc--[a-z]+/g, "") + " " + className;
            } else {
                classStr = className;
            }
            this.splice.setAttribute("class", classStr);
        }

        setSpliceState("bc--ready");
        this.loadImgs(this.imgs, (arr) => {
            setSpliceState("bc--start");
            this.imgObjs = arr;
            let minTrueDistance = null,
                maxTrueDistance = null;
            const 
                maxDistance = this.splice.offsetWidth - this.slideBtn.offsetWidth,
                updateFn = () => {
                    this.checking = false;
                    this.strip.style.width = this.slideBtn.offsetWidth + "px";
                    this.piece.style.transform = "translateX(0)";
                    this.piece.style.msTransform = "translateX(0)";

                    this.randomImgInfo.index = this.getRandomInt(0, this.imgObjs.length - 1, this.randomImgInfo.index);
                    this.randomImgInfo.obj = this.imgObjs[this.randomImgInfo.index];
                    this.randomRipInfo = {
                        x: this.getRandomInt(this.range.left, this.rip.width - this.size - this.range.right),
                        y: this.getRandomInt(this.range.top, this.rip.height - this.size - this.range.bottom)
                    };
                    minTrueDistance = this.randomRipInfo.x / this.scale - this.piece.offsetLeft - this.dis.left;
                    maxTrueDistance = this.randomRipInfo.x / this.scale - this.piece.offsetLeft + this.dis.right;
                    this.piece.style.top = this.randomRipInfo.y / this.scale + "px";
                    this.setRipArea({
                        finish: (x, y, width, height) => {
                            this.randomImgInfo.x = x;
                            this.randomImgInfo.y = y;
                            this.randomImgInfo.width = width;
                            this.randomImgInfo.height = height;
                        }
                    });
                    this.setPieceArea();
                },
                createBeforeFn = () => {
                    if(this.checking) {
                        return false;
                    }
                },
                progressFn = (distance) => {
                    this.strip.style.width = distance + this.slideBtn.offsetWidth + "px";
                    this.piece.style.msTransform = "translateX(" + distance + "px)";
                    this.piece.style.transform = "translateX(" + distance + "px)";
                },
                finishFn = (distance) => {
                    setSpliceState("bc--check");
                    this.checking = true;
                    // 操作成功的操作。
                    const success = () => {
                        setSpliceState("bc--success");
                        this.removeClass(this.screen, "bc--active");
                        this.success && this.success();
                    };
                    // 操作失败的操作。
                    const fail = () => {
                        setSpliceState("bc--fail");
                        const timer = setTimeout(() => {
                            clearTimeout(timer);
                            setSpliceState();
                            updateFn();
                        }, 800);
                        this.fail && this.fail();
                    };
                    if (distance >= minTrueDistance && distance <= maxTrueDistance) {
                        if (this.condition) {
                            this.condition(success, fail);
                        } else {
                            success();
                        }
                    } else {
                        fail();
                    }
                };

            updateFn();
            this.startPrompt.addEventListener("click", () => {
                this.addClass(this.screen, "bc--active");
                setSpliceState();
                updateFn();
            });
            this.closeBtn.addEventListener("click", () => {
                this.removeClass(this.screen, "bc--active");
                setSpliceState("bc--start");
            });
            this.updateBtn.addEventListener("click", () => {
                updateFn();
            });

            addEvent({
                object: this.slideBtn,
                maxDistance,
                createBefore: createBeforeFn,
                progress: progressFn,
                finish: finishFn
            });
            addEvent({
                object: this.piece,
                maxDistance,
                createBefore: createBeforeFn,
                progress: progressFn,
                finish: finishFn
            });
        });
    }
    // 小数转整数，同时四舍五入。
    setToInt(number) {
        return Math.floor(number + 0.5);
    }
    // 产生随机整数。
    getRandomInt(min, max, except) {
        let num = parseInt(Math.random() * (max + 1 - min) + min);
        while(num === max + 1 || num === except) {
            num = parseInt(Math.random() * (max + 1 - min) + min);
        }
        return num;
    }
    // 加载图片，其中imgs是一个数组。
    loadImgs(imgs, success) {
        let arr = [];
        let count = 0;
        for (let i = 0; i < imgs.length; i++) {
            const img = new Image();
            img.src = imgs[i];
            img.onload = function() {
                count++;
                arr.push(img);
                if (count === imgs.length) {
                    success && success(arr);
                }
            }
        }
    }
    removeClass(object, className) {
        let classStr = object.getAttribute("class");
        if (classStr !== null) {
            object.setAttribute("class", classStr.replace(new RegExp("\s*" + className, "g"), ""));
        }
    }
    addClass(object, className) {
        let classStr = object.getAttribute("class");
        if (classStr) {
            classStr += " " + className;
        } else {
            classStr = className;
        }
        object.setAttribute("class", classStr);
    }
    // 创建滑动事件，用于手机端。
    createSlideXTouchEvent({
        object,
        maxDistance,
        createBefore,
        progress,
        finish
    }) {
        let startX = null;
        let nowDistance = null;
        const down = (e) => {
            if (createBefore && createBefore() === false) {
                return;
            }
            startX = e.targetTouches[0].clientX;
            document.addEventListener("touchmove", move);
            document.addEventListener("touchend", up);
        }
        const move = (e) => {
            nowDistance = e.targetTouches[0].clientX - startX;
            if (nowDistance <= 0) {
                nowDistance = 0;
            } else if (nowDistance >= maxDistance) {
                nowDistance = maxDistance;
            }
            progress && progress(nowDistance);
        }
        const up = () => {
            finish && finish(nowDistance);
            document.removeEventListener("touchmove", move, {
                passive: true
            });
            document.removeEventListener("touchend", up);
        }
        object.addEventListener("touchstart", down, {
            passive: true
        });
    }
    // 创建鼠标滑动事件，用于pc端。
    createSlideXMouseEvent({
        object,
        maxDistance,
        createBefore,
        progress,
        finish
    }) {
        let startX = null;
        let nowDistance = null;
        const down = (e) => {
            if (createBefore && createBefore() === false) {
                return;
            }
            startX = e.clientX;
            e.preventDefault();
            document.addEventListener("mousemove", move);
            document.addEventListener("mouseup", up);
        }
        const move = (e) => {
            nowDistance = e.clientX - startX;
            if (nowDistance <= 0) {
                nowDistance = 0;
            } else if (nowDistance >= maxDistance) {
                nowDistance = maxDistance;
            }
            progress && progress(nowDistance);
            e.preventDefault();
        }
        const up = () => {
            finish && finish(nowDistance);
            document.removeEventListener("mousemove", move);
            document.removeEventListener("mouseup", up);
        }
        object.addEventListener("mousedown", down);
    }
    createDiv(object, className) {
        const div = document.createElement("div");
        this.addClass(div, className);
        object.appendChild(div);
        return div;
    }
    createCanvas(object, className, scale) {
        const canvas = document.createElement("canvas");
        this.addClass(canvas, className);
        object.appendChild(canvas);
        canvas.width = canvas.offsetWidth * scale;
        canvas.height = canvas.offsetHeight * scale;
        return canvas;
    }
    /*
        创建canvas中绘制缺口的路径。
        area：canvas.getContext("2d")；
        x/y：绘制的起点x/y坐标；
        size：缺口的宽高（宽高是一致的）。
    */
    createPiecePath(area, x, y, size) {
        const s = size / 164,
            r = Math.PI / 180,
            i = this.setToInt;
        area.beginPath();
        area.lineTo(i(x), i(44 * s + y));
        area.arc(i(120 / 2 * s + x), i(25 * s + y), 25 * s, -230 * r, 50 * r);
        area.lineTo(i(120 * s + x), i(44 * s + y));
        area.arc(i((120 + 19) * s + x), i((120 / 2 + 44) * s + y), i(25 * s), -140 * r, 140 * r);
        area.lineTo(i(120 * s + x), i(164 * s + y));
        area.lineTo(i(x), i(164 * s + y));
        area.lineTo(i(x), i((164 - 44) * s + y));
        area.arc(i(19 * s + x), i((120 / 2 + 44) * s + y), i(25 * s), 140 * r, -140 * r, true);
        area.closePath();
        return area;
    }
    // 设置
    setRipArea({ finish }) {
        let area    = this.ripArea,
            x       = this.randomImgInfo.x,
            y       = this.randomImgInfo.y,
            width   = this.randomImgInfo.obj.width,
            height  = this.randomImgInfo.obj.height;

        if (width / height > this.rip.width / this.rip.height) {
            width = this.rip.height * width / height;
            height = this.rip.height;
            x = (this.rip.width - width) / 2;
        } else {
            height = this.rip.width * height / width;
            width = this.rip.width;
            y = (this.rip.height - height) / 2;
        }
        area.clearRect(0, 0, Math.ceil(this.rip.width), Math.ceil(this.rip.height));
        area.beginPath();
        area.save();
        area.drawImage(
            this.randomImgInfo.obj, 
            this.setToInt(x), 
            this.setToInt(y), 
            this.setToInt(width), 
            this.setToInt(height)
        );
        area = this.createPiecePath(
            area, 
            this.randomRipInfo.x, 
            this.randomRipInfo.y, 
            this.size
        );
        area.shadowColor = "#000";
        area.shadowBlur = 10;
        area.fillStyle = "#fff";
        area.globalAlpha = 0.4;
        area.fill();
        area.restore();

        finish && finish(x, y, width, height);
    }
    setPieceArea() {
        let area = this.pieceArea;

        area.clearRect(0, 0, Math.ceil(this.size), Math.ceil(this.piece.height));
        area.beginPath();
        area.save();
        area = this.createPiecePath(area, 0, 0, this.size);
        area.clip();
        area.drawImage(
            this.randomImgInfo.obj, 
            -this.setToInt(this.randomRipInfo.x + (this.randomImgInfo.width - this.rip.width) / 2), 
            -this.setToInt(this.randomRipInfo.y + (this.randomImgInfo.height - this.rip.height) / 2), 
            this.setToInt(this.randomImgInfo.width), 
            this.setToInt(this.randomImgInfo.height)
        );
        area.globalAlpha = 0.5;
        area.lineWidth = 6;
        area.strokeStyle = "#fff";
        area.stroke();
        area.lineWidth = 2;
        area.strokeStyle = "#000";
        area.stroke();
        area.restore();
    }
}
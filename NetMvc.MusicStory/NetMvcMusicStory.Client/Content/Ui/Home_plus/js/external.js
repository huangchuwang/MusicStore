/* swiper轮播 */
var mySwiper = new Swiper('#case7', {
		loop : true,        //允许从第一张到最后一张，或者从最后一张到第一张  循环属性
		effect : 'coverflow',  //轮播效果，coverflow覆盖流效果
		slidesPerView :2, 
		centeredSlides: true,
		autoplay: true,  //可选选项，自动滑动
		initialSlide :1,//默认显示第二张图片索引从0开始
		speed:3000,//设置过度时间
		/* grabCursor: true,//鼠标样式根据浏览器不同而定 */
		 autoplay : {
			delay:3000,
			disableOnInteraction:false,
		  },                 /*  设置每隔3000毫秒切换 */
		// <!-- 分页器 -->
		 pagination: {
			  el: '.swiper-pagination', 
			  clickable :true,        /* 可点击切换 */
			  dynamicBullets: true,   /* 动力球样式 */
			},
		// <!-- 导航按钮 上一页下一页 -->
		 navigation: {
			  nextEl: '.swiper-button-next',
			  prevEl: '.swiper-button-prev',
			},
		// <!-- 滚动条 -->
		 scrollbar: {
			  el: '.swiper-scrollbar',
			  hide:true,
			},
		/*  设置当鼠标移入图片时是否停止轮播*/
		  coverflowEffect: {
			  rotate: 30,
			  stretch: 10,
			  depth: 60,
			  modifier: 2,
			  slideShadows : true,
			},
		 lazy: {
			loadPrevNext: true,
		  },
});


var mySwiper = new Swiper('#case8', {
	loop: true,        //允许从第一张到最后一张，或者从最后一张到第一张  循环属性
	effect: 'coverflow',  //轮播效果，coverflow覆盖流效果
	slidesPerView: 2,
	centeredSlides: true,
	autoplay: true,  //可选选项，自动滑动
	initialSlide: 1,//默认显示第二张图片索引从0开始
	speed: 3000,//设置过度时间
	/* grabCursor: true,//鼠标样式根据浏览器不同而定 */
	autoplay: {
		delay: 3000,
		disableOnInteraction: false,
	},                 /*  设置每隔3000毫秒切换 */
	// <!-- 分页器 -->
	pagination: {
		el: '.swiper-pagination',
		clickable: true,        /* 可点击切换 */
		dynamicBullets: true,   /* 动力球样式 */
	},
	// <!-- 导航按钮 上一页下一页 -->
	navigation: {
		nextEl: '.swiper-button-next',
		prevEl: '.swiper-button-prev',
	},
	// <!-- 滚动条 -->
	scrollbar: {
		el: '.swiper-scrollbar',
		hide: true,
	},
	/*  设置当鼠标移入图片时是否停止轮播*/
	coverflowEffect: {
		rotate: 30,
		stretch: 10,
		depth: 60,
		modifier: 2,
		slideShadows: true,
	},
	lazy: {
		loadPrevNext: true,
	},
});
/* 懒加载 */
$("#case7 img").lazyload();
$("#case8 img").lazyload();
$("#content img").lazyload();
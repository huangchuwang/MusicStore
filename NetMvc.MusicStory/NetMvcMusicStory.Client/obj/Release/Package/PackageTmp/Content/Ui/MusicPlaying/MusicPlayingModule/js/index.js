var count = 0;
var page_num = 1;
var num;
var current_pages = 1;
var limit = 5;
var id = $("#albumid").val();
var account = $("#useraccount").val();
var icon = $("#usericon").val();
var praisecount;
var PlayAdd = true;
Praise_Count();
GetMusicList(id, page_num);
GetCommentList(id);

if (account != "") {
	disk_img.src = icon;
	song_img.src = icon;
	$(".gc").css("background", "url(" + icon + ")");
	$(".gc").css("background-size", "15%");
}

//提示窗
//var top = ($(window).height() - $(divName).height()) / 2;
//var left = ($(window).width() - $(divName).width()) / 2;
//var scrollTop = $(document).scrollTop();
//var scrollLeft = $(document).scrollLeft();
//$("#disappare").css({
//	position: 'absolute', 'top': top + scrollTop, left: left + scrollLeft
//}).show();  

$(function () {
	$("#dianji").click(function () {
		$("#disappare").show().delay(3000).hide(300);
	});
})

//播放量增加
function PlayNumberAdd() {
	$.ajax({
		type: 'post',
		async: true,
		url: "/PlayMusic/PlayNumberAdd?albumid=" + id,
		datatype: 'json',
		success: function (data) {
			alert(data);
		}
	});
}

//分享弹窗
function Share_Open() {
	layer.open({
		type: 1,
		title: '分享歌曲',
		shadeClose: true,
		shade: false,//背景笼罩
		maxmin: true, //开启最大化最小化按钮
		area: ['500px', '20%'],
		content: '<div class= "form-group" style="text-align:center; margin-top:3%;" >' +
			'<label for="name">分享到</label></div><div style="text-align:center;margin-bottom:20px;">' +
			'<a href="http://connect.qq.com/widget/shareqq/index.html?url=' + window.location.href+"?albumid="+id+'&sharesource=qzone&title=给你听听这首歌&pics=图片&summary=阿里旺旺音乐&desc=阿里旺旺音乐"><svg style="margin:0 10px;" t="1610455659712" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="2527" width="50px" height="50px"><path d="M511.09761 957.257c-80.159 0-153.737-25.019-201.11-62.386-24.057 6.702-54.831 17.489-74.252 30.864-16.617 11.439-14.546 23.106-11.55 27.816 13.15 20.689 225.583 13.211 286.912 6.767v-3.061z" fill="#FAAD08" p-id="2528"></path><path d="M496.65061 957.257c80.157 0 153.737-25.019 201.11-62.386 24.057 6.702 54.83 17.489 74.253 30.864 16.616 11.439 14.543 23.106 11.55 27.816-13.15 20.689-225.584 13.211-286.914 6.767v-3.061z" fill="#FAAD08" p-id="2529"></path><path d="M497.12861 474.524c131.934-0.876 237.669-25.783 273.497-35.34 8.541-2.28 13.11-6.364 13.11-6.364 0.03-1.172 0.542-20.952 0.542-31.155C784.27761 229.833 701.12561 57.173 496.64061 57.162 292.15661 57.173 209.00061 229.832 209.00061 401.665c0 10.203 0.516 29.983 0.547 31.155 0 0 3.717 3.821 10.529 5.67 33.078 8.98 140.803 35.139 276.08 36.034h0.972z" fill="#000000" p-id="2530"></path><path d="M860.28261 619.782c-8.12-26.086-19.204-56.506-30.427-85.72 0 0-6.456-0.795-9.718 0.148-100.71 29.205-222.773 47.818-315.792 46.695h-0.962C410.88561 582.017 289.65061 563.617 189.27961 534.698 185.44461 533.595 177.87261 534.063 177.87261 534.063 166.64961 563.276 155.56661 593.696 147.44761 619.782 108.72961 744.168 121.27261 795.644 130.82461 796.798c20.496 2.474 79.78-93.637 79.78-93.637 0 97.66 88.324 247.617 290.576 248.996a718.01 718.01 0 0 1 5.367 0C708.80161 950.778 797.12261 800.822 797.12261 703.162c0 0 59.284 96.111 79.783 93.637 9.55-1.154 22.093-52.63-16.623-177.017" fill="#000000" p-id="2531"></path><path d="M434.38261 316.917c-27.9 1.24-51.745-30.106-53.24-69.956-1.518-39.877 19.858-73.207 47.764-74.454 27.875-1.224 51.703 30.109 53.218 69.974 1.527 39.877-19.853 73.2-47.742 74.436m206.67-69.956c-1.494 39.85-25.34 71.194-53.24 69.956-27.888-1.238-49.269-34.559-47.742-74.435 1.513-39.868 25.341-71.201 53.216-69.974 27.909 1.247 49.285 34.576 47.767 74.453" fill="#FFFFFF" p-id="2532"></path><path d="M683.94261 368.627c-7.323-17.609-81.062-37.227-172.353-37.227h-0.98c-91.29 0-165.031 19.618-172.352 37.227a6.244 6.244 0 0 0-0.535 2.505c0 1.269 0.393 2.414 1.006 3.386 6.168 9.765 88.054 58.018 171.882 58.018h0.98c83.827 0 165.71-48.25 171.881-58.016a6.352 6.352 0 0 0 1.002-3.395c0-0.897-0.2-1.736-0.531-2.498" fill="#FAAD08" p-id="2533"></path><path d="M467.63161 256.377c1.26 15.886-7.377 30-19.266 31.542-11.907 1.544-22.569-10.083-23.836-25.978-1.243-15.895 7.381-30.008 19.25-31.538 11.927-1.549 22.607 10.088 23.852 25.974m73.097 7.935c2.533-4.118 19.827-25.77 55.62-17.886 9.401 2.07 13.75 5.116 14.668 6.316 1.355 1.77 1.726 4.29 0.352 7.684-2.722 6.725-8.338 6.542-11.454 5.226-2.01-0.85-26.94-15.889-49.905 6.553-1.579 1.545-4.405 2.074-7.085 0.242-2.678-1.834-3.786-5.553-2.196-8.135" fill="#000000" p-id="2534"></path><path d="M504.33261 584.495h-0.967c-63.568 0.752-140.646-7.504-215.286-21.92-6.391 36.262-10.25 81.838-6.936 136.196 8.37 137.384 91.62 223.736 220.118 224.996H506.48461c128.498-1.26 211.748-87.612 220.12-224.996 3.314-54.362-0.547-99.938-6.94-136.203-74.654 14.423-151.745 22.684-215.332 21.927" fill="#FFFFFF" p-id="2535"></path><path d="M323.27461 577.016v137.468s64.957 12.705 130.031 3.91V591.59c-41.225-2.262-85.688-7.304-130.031-14.574" fill="#EB1C26" p-id="2536"></path><path d="M788.09761 432.536s-121.98 40.387-283.743 41.539h-0.962c-161.497-1.147-283.328-41.401-283.744-41.539l-40.854 106.952c102.186 32.31 228.837 53.135 324.598 51.926l0.96-0.002c95.768 1.216 222.4-19.61 324.6-51.924l-40.855-106.952z" fill="#EB1C26" p-id="2537"></path></svg></a>' +
			'<a href="https://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_onekey?url=' + window.location.href + "?albumid=" + id +'&sharesource=qzone&title=给你听听这首歌&pics=图片&summary=阿里旺旺音乐"><svg style="margin:0 10px;" t="1610455703538" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="2887" width="50px" height="50px"><path d="M955.728 428.224c8.385-8.785 3.76-23.536-8.073-25.753l-276.832-51.854c-4.838-0.906-9.02-3.987-11.38-8.383L525.873 93.229c-2.798-5.23-8.342-7.85-13.875-7.896-5.532 0.045-11.075 2.667-13.873 7.896L364.558 342.234c-2.36 4.396-6.543 7.477-11.381 8.383L76.345 402.471c-11.833 2.217-16.458 16.968-8.073 25.753L269.64 639.086c3.564 3.733 5.205 8.952 4.433 14.1l-46.015 282.032c-1.819 12.126 10.394 21.407 21.298 16.182L505 827.827a16.098 16.098 0 0 1 7-1.58 16.1 16.1 0 0 1 7.003 1.58L774.644 951.4c10.904 5.225 23.117-4.056 21.298-16.182l-46.88-287.298 206.666-219.696z" fill="#FFCD00" p-id="2888"></path><path d="M559.42 493.63c-4.517-3.772-110.987-40.332-273.968-16-3.16 0.47-5.913-0.394-8.04-1.992-0.717 4 3.587 7.152 8.988 7.527 115.064 8.021 179.42 54.987 199.492 71.501 40.78-28.923 71.882-50.606 73.063-51.527 3.668-2.856 3.695-6.811 0.465-9.51m135.65-29.972c-41.744 35.168-160.159 116.897-201.52 148.468-4.864 3.711-3.177 9.424 2.098 11.43 17.045 6.488 36.23 11.95 56.421 16.445l159.784-152.228c12.544-13.184 5.238-29.152-10.422-32.661-1.025 3.011-3.259 5.933-6.36 8.546M817.187 640l-0.101 0.045c-70.456 29.709-241.54 79.623-451.762 72.33-25.386-0.88-50.63-3.715-64.786-6.325-2.067-0.38-3.878-1.012-5.476-1.846-10.567 12.224 2.073 21.462 47.148 30.58 131.886 26.676 286.047 38.934 415.304 30.665l-8.884-54.324c43.727-31.431 64.996-58.546 67.524-62.57 2.899-4.616 1.033-8.555 1.033-8.555" fill="#F1A308" p-id="2889"></path><path d="M818.863 646.995c-53.31 5.137-215.894 3.686-311.826-33.167-5.107-1.962-6.834-7.566-2.129-11.194 40.025-30.84 154.62-110.68 195.014-145.035 7.872-6.696 9.95-15.437 0.375-22.542-18.36-13.623-83.168-36.203-158.198-36.816-107.373-0.88-212.858 29.498-259.133 54.09-10.983 5.837-4.392 21.221 6.83 19.495 164.223-25.24 271.495 12.756 276.045 16.67 3.255 2.798 3.074 6.906-0.5 9.715-3.036 2.389-199.263 143.36-258.23 193.11-9.286 7.834-6.845 24.246 8.35 27.018 14.152 2.582 39.406 5.412 64.784 6.284 210.173 7.214 381.314-42.24 451.755-71.63 0 0-2.148-7.057-13.137-5.998" fill="#FFFFFF" p-id="2890"></path></svg></a>' +
			'<a href="http://service.weibo.com/share/share.php?url=' + window.location.href + "?albumid=" + id +'&sharesource=weibo&title=给你听听这首歌&pic=图片"><svg style="margin:0 10px;" t="1610455727293" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="3749" width="50px" height="50px"><path d="M851.4 590.193c-22.196-66.233-90.385-90.422-105.912-91.863-15.523-1.442-29.593-9.94-19.295-27.505 10.302-17.566 29.304-68.684-7.248-104.681-36.564-36.14-116.512-22.462-173.094 0.866-56.434 23.327-53.39 7.055-51.65-8.925 1.89-16.848 32.355-111.02-60.791-122.395C311.395 220.86 154.85 370.754 99.572 457.15 16 587.607 29.208 675.873 29.208 675.873h0.58c10.009 121.819 190.787 218.869 412.328 218.869 190.5 0 350.961-71.853 398.402-169.478 0 0 0.143-0.433 0.575-1.156 4.938-10.506 8.71-21.168 11.035-32.254 6.668-26.205 11.755-64.215-0.728-101.66z m-436.7 251.27c-157.71 0-285.674-84.095-285.674-187.768 0-103.671 127.82-187.76 285.674-187.76 157.705 0 285.673 84.089 285.673 187.76 0 103.815-127.968 187.768-285.673 187.768z" fill="#E71F19" p-id="3750"></path><path d="M803.096 425.327c2.896 1.298 5.945 1.869 8.994 1.869 8.993 0 17.7-5.328 21.323-14.112 5.95-13.964 8.993-28.793 8.993-44.205 0-62.488-51.208-113.321-114.181-113.321-15.379 0-30.32 3.022-44.396 8.926-11.755 4.896-17.263 18.432-12.335 30.24 4.933 11.662 18.572 17.134 30.465 12.238 8.419-3.46 17.268-5.33 26.41-5.33 37.431 0 67.752 30.241 67.752 67.247 0 9.068-1.735 17.857-5.369 26.202a22.832 22.832 0 0 0 12.335 30.236l0.01 0.01z" fill="#F5AA15" p-id="3751"></path><path d="M726.922 114.157c-25.969 0-51.65 3.744-76.315 10.942-18.423 5.472-28.868 24.622-23.5 42.91 5.509 18.29 24.804 28.657 43.237 23.329a201.888 201.888 0 0 1 56.578-8.064c109.253 0 198.189 88.271 198.189 196.696 0 19.436-2.905 38.729-8.419 57.16-5.508 18.289 4.79 37.588 23.212 43.053 3.342 1.014 6.817 1.442 10.159 1.442 14.943 0 28.725-9.648 33.37-24.48 7.547-24.906 11.462-50.826 11.462-77.175-0.143-146.588-120.278-265.813-267.973-265.813z" fill="#F5AA15" p-id="3752"></path><path d="M388.294 534.47c-84.151 0-152.34 59.178-152.34 132.334 0 73.141 68.189 132.328 152.34 132.328 84.148 0 152.337-59.182 152.337-132.328 0-73.15-68.19-132.334-152.337-132.334zM338.53 752.763c-29.454 0-53.39-23.755-53.39-52.987 0-29.228 23.941-52.989 53.39-52.989 29.453 0 53.39 23.76 53.39 52.989 0 29.227-23.937 52.987-53.39 52.987z m99.82-95.465c-6.382 11.086-19.296 15.696-28.726 10.219-9.43-5.323-11.75-18.717-5.37-29.803 6.386-11.09 19.297-15.7 28.725-10.224 9.43 5.472 11.755 18.864 5.37 29.808z" fill="#040000" p-id="3753"></path></svg></a>' +
			'</div>'
	});
	Star();
}

//获取音乐数据
function GetMusicList(id, page) {
	page_num = page;
	$.ajax({
		type: 'get',
		async: true,
		url: "/PlayMusic/GetAllPlayMusic?albumid=" + id + "&page=" + page_num + "&limit=" + limit,
		datatype: 'json',
		success: function (data) {
			musics = [];
			var object = $.parseJSON(data);
			count = object.count;
			AlbumImg.src = object.data[0].AlbumImgUrl;
			$("#AlbumDe").html(object.data[0].AlbumDescription);
			$("#albumname").html(object.data[0].AlbumName);
			$("#artistname").html(object.data[0].ArtistName);
			$("#genrename").html(object.data[0].GenreName);
			$("#albumdate").html(object.data[0].AlbumDate);
			$("#playnumber").html(object.data[0].AlbumPlayNumber);
			for (var i = 0; i < object.data.length; i++) {
				$("#autime").append('<audio src="' + object.data[i].MusicUrl + '" id="au' + i + '"></audio>');
				var obj =
				{
					name: object.data[i].Name,
					singer: object.data[i].ArtistName,
					url: object.data[i].MusicUrl,
					lrc: object.data[i].Lrc,
					time: 0
				};
				musics.push(obj);
			}
			ergodic_music(musics);
		}, error: function (a) {
			ergodic_music(musics);
			alert("获取数据失败！请从主页面打开专辑列表！");
		}
	});
}

function ergodic_music(musics) {
	//遍历音乐列表
	$.each(musics, function (index, item) {
		var time = document.getElementById("au" + index);
		setTimeout(function () {
			$("#music-list").append('<li data-index=' + index + ' class="song_item flex_c">' +
				'<div class= "song_rank flex_c"><div class="rank_num">' +
				'<span class="playing" style="display:none;">' +
				'<span class="side1 pause"></span>' +
				'<span class="side2 pause"></span>' +
				'<span class="side3 pause"></span>' +
				'</span><span class="num" style="display:;">' + ++index +
				'</span ></div></div ><div class="song_name flex_c">' + item.name +
				'<i class="type iconfont icon-tag_wusun"></i>' +
				'<i class="has_mv iconfont icon-icon_mv_"></i></div >' +
				'<div class="song_artist"><span>' + item.singer +
				'</span ></div><div class="song_time"><span>' + getTime(parseInt(time.duration)) +
				'</span ></div></li >');
			index--;
			gc();
		}, 500);
	});

	//遍历页数
	if ($("#page li").length == 0) {
		$("#page").append('<li><span onclick="turn_page(0)">＜</span></li>');
		if (count % limit != 0) {
			num = count / limit + 1;
			num = Math.floor(num);
		}
		else {
			num = count / limit;
		}
		for (var i = 1; i <= num; i++) {
			$("#page").append('<li><span id="page' + i + '" class="" onclick="change_page(' + i + ')">' + i + '</span></li>');
		}
		$("#page").append('<li><span onclick="turn_page(1)">＞</span></li>');
	}
	$("#page" + current_pages).removeClass("notCursor currentPage");
	$("#page" + current_pages).attr('onclick', 'change_page(' + current_pages + ')');
	$("#page" + page_num).addClass("notCursor currentPage");
	$("#page" + page_num).attr('onclick', '');
	current_pages = page_num;

	//移除计算时间的音乐控件
	setTimeout(function () {
		$("#autime").empty();
	}, 3000);

	down(musics);
}


//获取评论数据
function GetCommentList(id) {
	$("#comment-list").empty();
	$("#comment-list").append('<div class="type"><span>最新评论</span ><span class="comment-num">条</span></div >');
	$.ajax({
		type: 'get',
		async: true,
		url: "/PlayMusic/GetAlbumComment?albumid=" + id,
		datatype: 'json',
		success: function (data) {
			comment = [];
			var object = $.parseJSON(data);
			$(".comment-num").html(object.count + "条");
			for (var i = 0; i < object.data.length; i++) {
				var obj =
				{
					useraccount: object.data[i].UserAccount,
					username: object.data[i].UserName,
					content: object.data[i].Content,
					usericon: object.data[i].UserIcon,
					create_time: object.data[i].Create_time,
					star: object.data[i].Star
				};
				comment.push(obj);
			}
			ergodic_comment(comment);
		}
	});
}
function ergodic_comment(comment) {
	//遍历评论列表
	$.each(comment, function (index, item) {
		if (item.useraccount == account) {
			$("#comment-list").append('<div class="each-comment"><div class= "each-comment-avatar" >' +
				'<img src="' + item.usericon + '" lazy="loaded"></div>' +
				'<div class= "each-comment-msg"><div class="each-comment-nickname">' +
				'<span>' + item.username + '<span class="youcomment" style="font-weight:400;">我的评论</span></span><iclass=""></i></div >' +
				'<div class="each-comment-content text-selection">' + item.content +
				'</div><div class="each-comment-time"><div class="time">' + item.create_time + '</div>' +
				'<div class="rating-container rating-gly-star" data-content="">' +
				'<div class="rating-stars" data-content="" style="width: ' + item.star * 20 + '%;">' +
				'</div ></div ></div></div></div >');
		}
		else {
			$("#comment-list").append('<div class="each-comment"><div class= "each-comment-avatar" >' +
				'<img src="' + item.usericon + '" lazy="loaded"></div>' +
				'<div class= "each-comment-msg"><div class="each-comment-nickname">' +
				'<span>' + item.username + '</span ><iclass=""></i></div >' +
				'<div class="each-comment-content text-selection">' + item.content +
				'</div><div class="each-comment-time"><div class="time">' + item.create_time + '</div>' +
				'<div class="rating-container rating-gly-star" data-content="">' +
				'<div class="rating-stars" data-content="" style="width: '+ item.star * 20 + '%;">'+
                '</div ></div ></div></div></div >');
		}
	});
}

function Comment_Open() {
	layer.open({
		type: 1,
		title: '发表评论',
		shadeClose: true,
		shade: false,//背景笼罩
		maxmin: true, //开启最大化最小化按钮
		area: ['800px', '30%'],
		content: '<div class= "form-group" style="text-align:center; margin-top:3%;" >' +
			'<label for="name">评论</label>' +
			'<input style="margin:auto 10%; width:80%;" type="text" class="form-control" id="content" placeholder="请输入评论内容，注意文明用语">' +
			'</div><div class="star-rating rating-lg rating-active" style="text-align:center;">' +
			'<div id="starzjd" class="rating-container rating-gly-star" data-content="">' +
			'<div id="rating-stars" class="rating-stars" data-content="" style="width: 100%;">' +
			'</div></div><div class="caption"><span id="star" class="label label-default">5</span>星' +
			'</div></div><br /><button type="button" class="btn btn-default" style="border-color: #CED4DA;margin:auto 45%; width:10%;" onclick="Comment()">发表</button>'
	}); 
	Star();
}

//发表评论
function Comment() {
	var content = $("#content").val();
	var star = $("#star").html();
	if (account == "") {
		alert("请登录后再进行评论！");
	} else if ($.trim(content) == "") {      //判断为空或只有空格
		alert("评论不能为空！");
	} else {
		//询问框
		layer.confirm('确定要发表本条评论吗？', {
			btn: ['发布', '取消'] //按钮
		}, function () {
			$.ajax({
				url: "/PlayMusic/CreateComment?albumid=" + id + "&account=" + account + "&content=" + content + "&star=" + star,
				type: "Post",
				async: true,
				success: function (data) {
					GetCommentList(id);
					$("#content").val("");
				}, error: function (data) {
				}
			});
			layer.msg('发布成功！', { icon: 1 });
		}, function () {
			layer.msg('取消成功！', { icon: 6 });
		});
    }
}

//点赞数量
function Praise_Count() {
	$.ajax({
		url: "/PlayMusic/PraiseCount?albumid=" + id,
		type: "Post",
		async: true,
		success: function (data) {
			praisecount = data;
		}
	});
	setTimeout(function () {
		inspect_Praise();
	}, 800);
}

//点赞
function Praise() {
	if (account == "") {
		alert("请登录后再进行点赞！");
	} else {
		$.ajax({
			url: "/PlayMusic/HandlePraise?albumid=" + id + "&account=" + account,
			type: "Post",
			async: true,
			success: function () {
				Praise_Count();	
				inspect_Praise();
			}
		});
	}
}

//修改点赞图标
function inspect_Praise() {
	if (account != "") {
		$.ajax({
			url: "/PlayMusic/Inspect_Praise?albumid=" + id + "&account=" + account,
			type: "Post",
			async: true,
			datatype: 'json',
			success: function (data) {
				if (data == "True") {
					$("#praise").html('<svg style="height:20px;width:20px;" t="1609854930523" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="1116" width="200" height="200"><path d="M694.830025 110.467951c-76.670194 0-137.458675 38.335097-184.008874 101.313453-47.096644-62.978357-107.339704-101.313453-184.009897-101.313453-138.554636 0-251.372098 122.126478-251.372098 272.730545 0 89.814562 40.524972 152.793942 72.839959 203.178469 93.64582 147.31516 319.826166 305.58686 329.682651 313.254493 9.856485 7.667633 20.810978 10.951423 32.30987 10.951423 11.502985 0 22.454408-3.831258 32.30987-10.951423 9.860579-7.667633 236.036831-166.484755 330.23626-313.254493 32.30987-50.385551 72.833819-113.364931 72.833819-203.178469C945.653631 233.141898 832.842309 110.467951 694.830025 110.467951L694.830025 110.467951 694.830025 110.467951 694.830025 110.467951 694.830025 110.467951z" p-id="1117" fill="#d81e06"></path></svg><span>已点赞</span>');
					$("#praise2").html('<svg style="height:18px;width:18px;" t="1609854930523" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="1116" width="200" height="200"><path d="M694.830025 110.467951c-76.670194 0-137.458675 38.335097-184.008874 101.313453-47.096644-62.978357-107.339704-101.313453-184.009897-101.313453-138.554636 0-251.372098 122.126478-251.372098 272.730545 0 89.814562 40.524972 152.793942 72.839959 203.178469 93.64582 147.31516 319.826166 305.58686 329.682651 313.254493 9.856485 7.667633 20.810978 10.951423 32.30987 10.951423 11.502985 0 22.454408-3.831258 32.30987-10.951423 9.860579-7.667633 236.036831-166.484755 330.23626-313.254493 32.30987-50.385551 72.833819-113.364931 72.833819-203.178469C945.653631 233.141898 832.842309 110.467951 694.830025 110.467951L694.830025 110.467951 694.830025 110.467951 694.830025 110.467951 694.830025 110.467951z" p-id="1117" fill="#d81e06"></path></svg><span class="praise_count">' + praisecount + '</span');
				} else {
					$("#praise").html('<i class="iconfont icon-bar_icon_heart_"></i><span>点赞</span>');
					$("#praise2").html('<i title="点赞歌曲" class="prev icon_heart iconfont icon-bar_icon_heart_"></i><span class="praise_count">' + praisecount + '</span');
				}
			}, error: function () {
				$("#praise").html('<i class="iconfont icon-bar_icon_heart_"></i><span>点赞</span>');
				$("#praise2").html('<i title="点赞歌曲" class="prev icon_heart iconfont icon-bar_icon_heart_"></i><span class="praise_count">' + praisecount + '</span');
				alert("检索失败！");
			}
		});
	} else {
		$("#praise").html('<i class="iconfont icon-bar_icon_heart_"></i><span>点赞</span>');
		$("#praise2").html('<i title="点赞歌曲" class="prev icon_heart iconfont icon-bar_icon_heart_"></i><span class="praise_count">' + praisecount + '</span');
	}
}


function change_page(page_num) {
	switch_play();
	GetMusicList(id, page_num);
}

function turn_page(state) {
	if (num == 1) {
		alert("专辑只有这一页啦！");
	}
	else {
		if (state == 0) {
			page_num--;
			if (page_num == 0) {
				page_num = num;
			}
		} else if (state == 1) {
			page_num++;
			if (page_num > num) {
				page_num = 1;
			}
		}
		GetMusicList(id, page_num);
		switch_play();
	}
}

function down(musics) {

	//启动时播放的音乐  只设置时间 和名称
	au.src = musics[0].url;
	$("#music-name").text(musics[0].name + " - " + musics[0].singer);
	get = true;

	//延时一下 等待播放器初始化
	setTimeout(function () {
		$("#time").text(getTime(au.currentTime) + "/" + getTime(parseInt(au.duration)));
		//循环拼接歌词
		appendLrc(0);
	}, 500);

	//拼接歌词
	appendLrc(0);
}

var index = 0; //记录当前播放歌曲的下标
var get = true; //是否拿到歌词标记
var bf = false; //是否播放标记
//播放按钮
$("#play").click(function () {
	play();
})
//下一个
$("#next").click(function () {
	$("#jd").css('width', '0%');
	stop_it();
	next();
});

//上一个
$("#last").click(function () {
	$("#jd").css('width', '0%');
	stop_it();
	last();
});

$("#Control_icon").click(function () {
	if ($('#playControl').is('.nolock')) {
		$("#playControl").attr("class", "playControl islock");
		$("#Control_icon").attr("class", "iconfont icon-bar_icon_lock_");
	}
	else {
		$("#playControl").attr("class", "playControl nolock");
		$("#Control_icon").attr("class", "iconfont icon-bar_icon_unlock_");
	}
});

//下一首
function next() {
	PlayAdd = true;
	index++;
	if (index >= musics.length) {
		index = 0;
	}
	au.src = musics[index].url;
	$("#music-name").text(musics[index].name + " - " + musics[index].singer)
	$("#nowlrc").text("");
	/*console.log(au.duration)
	var time = "00:00/" + getTime(parseInt(au.duration));
	//设置当前时间和总时间
	*/
	$("#time").text("00:00/00:00");
	get = true;

	//延时一下 等待播放器初始化
	setTimeout(function () {
		$("#time").text(getTime(au.currentTime) + "/" + getTime(parseInt(au.duration)));
		//循环拼接歌词
	}, 500);

	//拼接歌词
	appendLrc(index);
	if (bf) {
		//修改图标
		$("#playicon").attr("class", "iconfont icon-bar_icon_pause_");
		//播放音乐
		au.play();

	} else {
		//修改图标
		$("#playicon").attr("class", "iconfont icon-bar_icon_play_1");
		//暂停
		au.pause();
	}
}
//上一首
function last() {
	PlayAdd = true;
	index--;
	if (index < 0) {
		index = musics.length - 1;
	}

	au.src = musics[index].url;
	$("#music-name").text(musics[index].name + " - " + musics[index].singer)
	$("#nowlrc").text("");
	//	var time = "00:00/" + getTime(parseInt(au.duration));
	//	//设置当前时间和总时间
	$("#time").text("00:00/00:00");
	get = true;

	//延时一下 等待播放器初始化
	setTimeout(function () {
		$("#time").text(getTime(au.currentTime) + "/" + getTime(parseInt(au.duration)));
		//循环拼接歌词
	}, 500);

	//拼接歌词
	appendLrc(index);
	if (bf) {
		//修改图标
		$("#playicon").attr("class", "iconfont icon-bar_icon_pause_");
		//播放音乐
		au.play();

	} else {
		//修改图标
		$("#playicon").attr("class", "iconfont icon-bar_icon_play_1");
		//暂停
		au.pause();
	}
}
//播放//或暂停
function play() {
	if (account == "") {
		alert("登录后即可播放音乐");
	} else {

		if (au.readyState == 0) {
			alert("未选中音乐！");
		} else {
			if (au.paused) {
				//修改图标
				$("#playicon").attr("class", "iconfont icon-bar_icon_pause_");
				//播放音乐
				au.play();
				bf = true;
			} else {
				//修改图标
				$("#playicon").attr("class", "iconfont icon-bar_icon_play_1");
				//暂停
				au.pause();
				bf = false;
			}
		}
	}
}

function switch_play() {
	//修改图标
	$("#playicon").attr("class", "iconfont icon-bar_icon_play_1");
	//播放音乐
	au.play();


	//延时一下 等待播放器初始化
	setTimeout(function () {
		$("#jd").css('width', '0%');
		$("#music-list").empty();
	}, 500);
}
//把秒转换成 分:秒 的形式
function getTime(s) {
	var ss = 0;
	var mm = 0;
	if (s >= 60) {

		if (s % 60 == 0) {
			ss = 0;
			mm = s / 60;
		} else {
			mm = parseInt(s / 60);
			ss = s - mm * 60;
		}
	} else {
		ss = s;
		mm = 0;
	}
	if (ss == NaN) {
		ss = "00";
	}
	if (mm < 10) {
		mm = "0" + mm;
	}
	if (ss < 10) {
		ss = "0" + ss;
	}

	return mm + ":" + ss;

}




//播放器的时间改变时间
au.ontimeupdate = function () {
	timeUpdate();
}

function downsong() {
	window.open(au.currentSrc);
}

function timeUpdate() {
	//获取播放器当前播放到的时间
	var dt = parseInt(au.currentTime);

	var time = getTime(dt) + "/" + getTime(parseInt(au.duration));
	//设置当前时间和总时间
	$("#time").text(time);
	//歌词数组变量
	var lrcArr;
	//判断 获取歌词方法返回的值是不是undefined
	if (getLRC(getTime(dt), musics[index].lrc) != undefined) {
		//拿到歌词数组 根据 & 分割 [0]:为歌词文本 [1]:为歌词下标
		lrcArr = getLRC(getTime(dt), musics[index].lrc).split("&");
		//设置文本歌词
		$("#nowlrc").text(lrcArr[1]);
		//调用歌词同步方法 参数 getLRC(getTime(dt)
		lrcSynchronization(lrcArr[0]);
	}

	var songstr = "#music-list li[data-index='" + index + "'] .song_rank .rank_num ";
	if (!au.paused) {
		$(songstr + ".playing").css("display", "");
		$(songstr + ".num").css("display", "none");

		var x = 10;//上限
		var y = 2; //下限
		$(songstr + ".playing .side1").css("height", parseInt(Math.random() * (x - y + 1) + y) + "px");
		$(songstr + ".playing .side2").css("height", parseInt(Math.random() * (x - y + 1) + y) + "px");
		$(songstr + ".playing .side3").css("height", parseInt(Math.random() * (x - y + 1) + y) + "px");
	} else {
		$(".side1").css("height", "6px");
		$(".side2").css("height", "4px");
		$(".side3").css("height", "6px");
		$(songstr + ".playing").css("display", "none");
		$(songstr + ".num").css("display", "");
	}

	//计算百分比(当前时间/总时间 )x 100%
	var bfb = (au.currentTime / au.duration);
	//设置进度条
	$("#jd").css("width", 370 * bfb + "px");
	//进度条控制
	//判断是否已经播放到末尾 如果是则下一首 还要判断是否是循环播放  
	//设置循环播放 通过本地存储 保存 一遍下一次打开自动会设置
	if (au.ended) {
		if (PlayAdd) {
			PlayNumberAdd();
		} else {
			PlayAdd = true;
		}
		//下一首音乐
		next();

	}
}

function stop_it() {
	var songstr = "#music-list li[data-index='" + index + "'] .song_rank .rank_num ";
	$(".side1").css("height", "6px");
	$(".side2").css("height", "4px");
	$(".side3").css("height", "6px");
	$(songstr + ".playing").css("display", "none");
	$(songstr + ".num").css("display", "");
}


function gc() {
	//拿到歌词列表的div
	var m_list = $(".song_item");
	//拿到歌曲列表
	$.each(m_list, function (i1, item) {
		$(item).dblclick(function () {
			PlayAdd = true;
			$(".song_item").removeClass("active");
			stop_it();

			$(this).addClass("active");

			index = this.dataset.index;
			//
			au.src = musics[index].url;
			$("#music-name").text(musics[index].name + " - " + musics[index].singer)
			//	var time = "00:00/" + getTime(parseInt(au.duration));
			//	//设置当前时间和总时间
			$("#time").text("00:00/00:00");
			get = true;
			//拼接歌词
			appendLrc(index);
			if (bf) {

				//修改图标
				$("#playicon").attr("class", "iconfont icon-bar_icon_pause_");
				//播放音乐
				au.play();

			} else {
				//修改图标
				$("#playicon").attr("class", "iconfont icon-bar_icon_play_1");
				//暂停
				au.pause();
			}

			//延时一下 等待播放器初始化
			setTimeout(function () {
				$("#time").text(getTime(au.currentTime) + "/" + getTime(parseInt(au.duration)));
				//循环拼接歌词
			}, 200);
			setTimeout(function () {
				if (account != "") {
					$("#playicon").attr("class", "iconfont icon-bar_icon_pause_");
					au.play();
				}
			}, 250);
		});
	});
}

//定义歌词数组 只拿一次
//从服务器拿歌词
//根据url 获取歌词数组
function getLrcs(file) {
	var lrcs;
	$.ajax({
		type: "get",
		dataType: "text",
		url: "/PlayMusic/Getgc?file=" + file,
		async: false,
		success: function (data) {
			//得到歌词数组
			lrcs = data.split("\n");
		},
		error: function () {
			alert("获取歌词失败");
		}
	});
	get = false;
	return lrcs;

}

//传入时间格式 xx:xx	
//传入时间和歌词文件的url 返回歌词字符串
//歌词数组
var lrcs;

//获取歌词 时间 歌词url
function getLRC(time, url) {
	//判断传入的url是不是为空 如果为空的话 就是没有歌词
	if (url.length != 0) {

		var lrcRes = "";
		if (get) {
			lrcs = getLrcs(url);
		}
		//遍历歌词数组
		for (var i = 0; i < lrcs.length; i++) {
			var lrc = lrcs[i];
			//拿到时间
			var t = lrc.substring(lrc.indexOf("[") + 1, lrc.lastIndexOf("]"));

			//去掉毫秒
			t = t.substring(0, t.indexOf("."));
			lrcRes = lrc;
			//如果传入的时间刚好是 这条歌词的时间 就返回
			if (time.trim() == t.trim()) {
				lrcRes = lrcRes.substring(lrcRes.indexOf("]") + 1, lrcRes.length + 1);
				if (lrcRes.trim().length == 0) {
					lrcRes = lrcs[i - 1].substring(lrcs[i - 1].indexOf("]") + 1, lrcs[i - 1].length + 1);
					return i - 1 + "&" + lrcRes;
				}

				return i + "&" + lrcRes;
			}
		}
	} else {
		return "-1&此歌曲没有歌词";
	}

	/*$.each(lrcs, function(i, lrc) {
	});*/

}

//拼接歌词到列表
function appendLrc(i) {

	$("#kfc-list-ul").html("");
	//也先判断是不是有歌词 
	if (musics[i].lrc.length != 0) {
		if (get) {
			lrcs = getLrcs(musics[i].lrc);
		}
		$.each(lrcs, function (index, item) {
			//歌词内容
			var name = item.substring(item.indexOf("]") + 1, item.length + 1);
			//拼接到div
			$("#kfc-list-ul").append("<li >" + name + "</li>")
		});
	} else {
		$("#kfc-list-ul").append("<li>此歌曲没有歌词</li>")
	}

}

//歌词同步方法 参数  歌词下标
function lrcSynchronization(lrcIdx) {
	if (lrcIdx != undefined && lrcIdx != -1) {
		var lis = $("#kfc-list-ul li");

		if (lrcIdx >= 1) {
			var lrc = $(lis[(parseInt(lrcIdx) - 1)]).text().trim();
			var lrc2 = $(lis[(parseInt(lrcIdx) + 1)]).text().trim();
			if (lrcIdx > 9) {
				// 如果下一句是空
				var top = (240 - (parseInt(lrcIdx) * 25));
				$("#kfc-list-ul").css("transform",
					"translateY(" + top + "px)");

			} else {
				$("#kfc-list-ul").css("transform", "translateY(0px)");
			}
			$(".kfc-list").scrollTop(0);

			$(lis).removeClass("nowlrc");

			$(lis[lrcIdx]).attr("class", "nowlrc");
		}

	}
}

//控制歌曲进度条
$(".wc").click(function (e) {
	//当前进度条宽度
	var w = e.pageX - $("#progress").offset().left;
	//计算百分比
	var bfb = (w / $("#progress").width());

	au.currentTime = au.duration * bfb;
	$("#jd").width(w + "px");
	PlayAdd = false;
})

//控制音量
$("#volumejdt").click(function (e) {
	var w = e.pageX - $("#volumejdt").offset().left;
	//计算音量
	var bfb = (w / $("#volumejdt").width());
	au.volume = bfb;
	$("#volumejd").width(w + "px");
})

au.volume = 0.5;
//控制静音
$(".volume_icon").click(function (e) {
	if (!au.muted) {
		$("#mute").css("display", "");
		$("#relieve").css("display", "none");
		au.muted = true;
	}
	else {
		$("#mute").css("display", "none");
		$("#relieve").css("display", "");
		au.muted = false;
	}
})

//拖动滚动条
//进度条宽度
//$("#yuan").mousedown(function(e) {
//
//	//获取当前进度条宽度 并保存
//	//	w = e.pageX - $("#progress").offset().left;
//	var w = e.pageX - $("#progress").offset().left;
//	var bfb = w / ($("#progress").width());
//	$("#jd").width((bfb * 100) + 1 + "%")
//})

function leftHidden() {

}

//评星
function Star() {
	$("#starzjd").click(function (e) {
		//当前宽度
		var w = e.pageX - $("#starzjd").offset().left;
		//计算百分比
		var bfb = (w / $("#starzjd").width() * 100);
		$("#rating-stars").width(bfb + "%");
		bfb = Math.round(bfb / 20 * 10) / 10;
		if (bfb > 5)
			bfb = 5;
		$("#star").html(bfb);
	})
}
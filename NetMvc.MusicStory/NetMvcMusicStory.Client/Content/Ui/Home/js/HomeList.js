var EnglishNum = new Array("One", " Two", " Three", " Four", "Five", "Six");


setTimeout(function () {
    GetNewest();
    GetFlow();
    GetHot();
    GetEvaluate();
    GetPraise();
    GetGenre("流行");
}, 500);

$(document).ready(function () {
    $("#slide_btn_1").on('click', function () {
        HideList();
        $('#newest').css('display', '');
    });
    $("#slide_btn_2").on('click', function () {
        HideList();
        $('#flow').css('display', '');
    });
    $("#slide_btn_3").on('click', function () {
        HideList();
        $('#hot').css('display', '');
    });
    $("#slide_btn_4").on('click', function () {
        HideList();
        $('#evaluate').css('display', '');
    });
    $("#slide_btn_5").on('click', function () {
        HideList();
        $('#praise').css('display', '');
    });
    $("#slide_btn_6").on('click', function () {
        HideList();
        $('#genre').css('display', '');
    });
})

//隐藏所有表单
function HideList() {
    $('#newest').css('display', 'none');
    $('#flow').css('display', 'none');
    $('#hot').css('display', 'none');
    $('#evaluate').css('display', 'none');
    $('#praise').css('display', 'none');
    $('#genre').css('display', 'none');
}


//最新榜单
function GetNewest() {
	$.ajax({
		type: 'get',
		async: true,
		url: "/Home/Newest_list",
		datatype: 'json',
        success: function (data) {
			albums = [];
            var object = $.parseJSON(data);
			for (var i = 0; i < object.data.length; i++) {
				var obj =
				{
					Id: object.data[i].Id,
					Name: object.data[i].Name,
                    ArtistName: object.data[i].ArtistName,
                    UrlString: object.data[i].UrlString
				};
				albums.push(obj);
			}
			Newest(albums);
		}, error: function (a) {
			alert("最新列表获取失败！");
		}
	});
}
//遍历最新榜单
function Newest(datas) {
    $new = '';
    for (var i = 0; i < 4; i++) {
        $new += '<div style="width:400px" class="display:inline-block;float: left;">';
        $new += '<label  for="slide_img" class="pic">';
        $new += '<img  src=' + datas[i].UrlString + ' alt="" class="img_1">';
        $new += '<div style="text-align:center;" class="card-body">';
        $new += '<h4 style="color:chartreuse">最新专辑</h4></br></br>';
        $new += '<a href="/PlayMusic/Index?albumid=' + datas[i].Id + '"><h4 ><font style="color:darkgoldenrod">' + EnglishNum[i] + '：' + datas[i].Name + '</font ></h4 ></a > ';
        $new += '<h4>歌手：' + datas[i].ArtistName + '</h4>';
        $new += '</div>';
        $new += '</label></div>';
    }
    $("#newest").append($new);
}


//流量榜单
function GetFlow() {
    $.ajax({
        type: 'get',
        async: true,
        url: "/Home/Flow_list",
        datatype: 'json',
        success: function (data) {
            albums = [];
            var object = $.parseJSON(data);
            for (var i = 0; i < object.data.length; i++) {
                var obj =
                {
                    Id: object.data[i].Id,
                    Name: object.data[i].Name,
                    ArtistName: object.data[i].ArtistName,
                    UrlString: object.data[i].UrlString
                };
                albums.push(obj);
            }
            Flow(albums);
        }, error: function (a) {
            alert("流量列表获取失败！");
        }
    });
}
//遍历流量榜单
function Flow(datas) {
    $new = '';
    for (var i = 0; i < 4; i++) {
        $new += '<div style="width:400px" class="display:inline-block;float: left;">';
        $new += '<label  for="slide_img" class="pic">';
        $new += '<img  src=' + datas[i].UrlString + ' alt="" class="img_2">';
        $new += '<div style="text-align:center;" class="card-body">';
        $new += '<h4 style="color:chartreuse">流量专辑</h4></br></br>';
        $new += '<a href="/PlayMusic/Index?albumid=' + datas[i].Id + '"><h4 ><font style="color:darkgoldenrod">' + EnglishNum[i] + '：' + datas[i].Name + '</font ></h4 ></a > ';
        $new += '<h4>歌手：' + datas[i].ArtistName + '</h4>';
        $new += '</div>';
        $new += '</label></div>';
    }
    $("#flow").append($new);
}


//热门榜单
function GetHot() {
    $.ajax({
        type: 'get',
        async: true,
        url: "/Home/Hot_list",
        datatype: 'json',
        success: function (data) {
            albums = [];
            var object = $.parseJSON(data);
            for (var i = 0; i < object.data.length; i++) {
                var obj =
                {
                    Id: object.data[i].Id,
                    Name: object.data[i].Name,
                    ArtistName: object.data[i].ArtistName,
                    UrlString: object.data[i].UrlString
                };
                albums.push(obj);
            }
            Hot(albums);
        }, error: function (a) {
            alert("热门列表获取失败！");
        }
    });
}
//遍历热门榜单
function Hot(datas) {
    $new = '';
    for (var i = 0; i < 4; i++) {
        $new += '<div style="width:400px" class="display:inline-block;float: left;">';
        $new += '<label  for="slide_img" class="pic">';
        $new += '<img  src=' + datas[i].UrlString + ' alt="" class="img_3">';
        $new += '<div style="text-align:center;" class="card-body">';
        $new += '<h4 style="color:chartreuse">热门专辑</h4></br></br>';
        $new += '<a href="/PlayMusic/Index?albumid=' + datas[i].Id + '"><h4 ><font style="color:darkgoldenrod">' + EnglishNum[i] + '：' + datas[i].Name + '</font ></h4 ></a > ';
        $new += '<h4>歌手：' + datas[i].ArtistName + '</h4>';
        $new += '</div>';
        $new += '</label></div>';
    }
    $("#hot").append($new);
}


//好评榜单
function GetEvaluate() {
    $.ajax({
        type: 'get',
        async: true,
        url: "/Home/Evaluate_list",
        datatype: 'json',
        success: function (data) {
            albums = [];
            var object = $.parseJSON(data);
            for (var i = 0; i < object.data.length; i++) {
                var obj =
                {
                    Id: object.data[i].Id,
                    Name: object.data[i].Name,
                    ArtistName: object.data[i].ArtistName,
                    UrlString: object.data[i].UrlString
                };
                albums.push(obj);
            }
            Evaluate(albums);
        }, error: function (a) {
            alert("好评列表获取失败！");
        }
    });
}
//遍历好评榜单
function Evaluate(datas) {
    $new = '';
    for (var i = 0; i < 4; i++) {
        $new += '<div style="width:400px" class="display:inline-block;float: left;">';
        $new += '<label  for="slide_img" class="pic">';
        $new += '<img  src=' + datas[i].UrlString + ' alt="" class="img_4">';
        $new += '<div style="text-align:center;" class="card-body">';
        $new += '<h4 style="color:chartreuse">好评专辑</h4></br></br>';
        $new += '<a href="/PlayMusic/Index?albumid=' + datas[i].Id + '"><h4 ><font style="color:darkgoldenrod">' + EnglishNum[i] + '：' + datas[i].Name + '</font ></h4 ></a > ';
        $new += '<h4>歌手：' + datas[i].ArtistName + '</h4>';
        $new += '</div>';
        $new += '</label></div>';
    }
    $("#evaluate").append($new);
}


//点赞榜单
function GetPraise() {
    $.ajax({
        type: 'get',
        async: true,
        url: "/Home/Praise_list",
        datatype: 'json',
        success: function (data) {
            albums = [];
            var object = $.parseJSON(data);
            for (var i = 0; i < object.data.length; i++) {
                var obj =
                {
                    Id: object.data[i].Id,
                    Name: object.data[i].Name,
                    ArtistName: object.data[i].ArtistName,
                    UrlString: object.data[i].UrlString
                };
                albums.push(obj);
            }
            Praise(albums);
        }, error: function (a) {
            alert("点赞列表获取失败！");
        }
    });
}
//遍历点赞榜单
function Praise(datas) {
    $new = '';
    for (var i = 0; i < 4; i++) {
        $new += '<div style="width:400px" class="display:inline-block;float: left;">';
        $new += '<label  for="slide_img" class="pic">';
        $new += '<img  src=' + datas[i].UrlString + ' alt="" class="img_5">';
        $new += '<div style="text-align:center;" class="card-body">';
        $new += '<h4 style="color:chartreuse">点赞专辑</h4></br></br>';
        $new += '<a href="/PlayMusic/Index?albumid=' + datas[i].Id + '"><h4 ><font style="color:darkgoldenrod">' + EnglishNum[i] + '：' + datas[i].Name + '</font ></h4 ></a > ';
        $new += '<h4>歌手：' + datas[i].ArtistName + '</h4>';
        $new += '</div>';
        $new += '</label></div>';
    }
    $("#praise").append($new);
}

//流派榜单
function GetGenre(genrename) {
    $.ajax({
        type: 'post',
        async: true,
        url: "/Home/Genre_list?genrename="+genrename,
        datatype: 'json',
        success: function (data) {
            albums = [];
            var object = $.parseJSON(data);
            var obj =
            {
                Id: object.data.Id,
                Name: object.data.Name,
                Description: object.data.Description,
                ArtistName: object.data.ArtistName,
                GenreName: object.data.GenreName,
                UrlString: object.data.UrlString,
                PlayNumber: object.data.PlayNumber
            };
            albums.push(obj);
            Genre(albums);
        }, error: function (a) {
            alert("流派列表获取失败！");
        }
    });
}
//遍历流派榜单
function Genre(datas) {
    $("#genrename").html(datas[0].GenreName + "巅峰榜");
    $("#albumname").html("专辑名：" + datas[0].Name);
    $("#genreartistname").html("歌手：" + datas[0].ArtistName);
    $("#genreplaynum").html("播放量：" + datas[0].PlayNumber);
    $("#genrede").html("专辑介绍：" + datas[0].Description);
    genreurl.src = datas[0].UrlString;
}

$(function () {
    var listurl = Easybuy.getUrl("test", "GetList");

    var listData = [];
    //get data from api
    Easybuy.get(listurl, null, function (res) {
        if (res.Success) {
            listData = res.Model;
        }
    });

    //var getUrl = Easybuy.getUrl("test", "Get");

    //var model = {};
    //Easybuy.get(getUrl, { id: 1 }, function (res) {
    //    model = res.model;
    //});

    var app = new Vue({
        el: "#main",
        data: {
            list: listData,
        },
        methods: {
            redirect: function (num) {
                var url = "_Admin/View/TestDetail.html?num="+num;
                window.location.href = url;
            }
        }
    });
});
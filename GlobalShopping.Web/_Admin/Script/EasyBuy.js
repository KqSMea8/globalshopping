Easybuy = {
    get: function (url,data,success) {
        $.ajax({
            url: url,
            data: data,
            type: "GET",
            async: false,
            success:success
        })
    },
    post: function (url,data,success) {
        $.ajax({
            url: url,
            data: data,
            type: "POST",
            async: false,
            success: success
        })
    },
    upload: function (url,data,success) {
        $.ajax({
            url: url,
            data: data,
            type: "POST",
            cache:false,
            contentType:"multipart/form-data",
            success: success

        })
    },
    postasync: function (url,data,success) {
        $.ajax({
            url: url,
            data: data,
            async:true,
            type: "POST",
            success: success
        })
    },
    getUrl: function (name,method) {
        return "/_api/" + name + "/" + method;
    },
    getQueryString : function (name, source) {
        if (!name) {
            return null;
        }
        source = source || window.location.search.substr(1);
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = source.match(reg);
        if (r != null) {
            return (r[2]);
        }
        return null;
    }
}

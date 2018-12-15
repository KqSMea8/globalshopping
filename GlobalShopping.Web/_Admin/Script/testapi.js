function test() {
    var data = new FormData();
    data.append("aa", "1");
    data.append("bb", "2");
    var paramurl = Easybuy.getUrl("test", "Testparam");

    var paramdata = {
        num: 1,
        message: "cz"
    };
    debugger;
    Easybuy.get(paramurl, paramdata, function (res) {
        debugger;
    });

    Easybuy.post(paramurl, paramdata, function (res) {
    });

    var url = Easybuy.getUrl("test", "Test");

    var data = {
        desc: "test",
        model: JSON.stringify({
            name: "test",
            num:"2"
        })
    }
    Easybuy.get(url, data, function (res) {
        debugger;
    });

    Easybuy.post(url, data, function (res) {
    });
    //model = {
    //    message: "cz",
    //    num: 1
    //};
    //data = JSON.stringify(model);
    //Easybuy.get(url, model, function (res) {
    //});
    var data = {
        desc: '1',
        model: JSON.stringify({
            name: 'name',
            num:1
        })
    }
    Easybuy.get(url, data, function (res) {
        debugger;
    });

    //Easybuy.upload(url, data, function (res) {
    //    debugger;
    //});
}

function testFile() {
    var form = new FormData();
    form.append("content", $("input[name=content]").val());
    form.append("file", $("input[name=file]").val());

    var url = Easybuy.getUrl("test", "uploadFile");
    Easybuy.upload(url, FormData, function (res) {
    });
}
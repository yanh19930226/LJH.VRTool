﻿layui.use(['form', 'jquery'], function () {
    var $ = layui.jquery;
    var form = layui.form;
   
    //全选
    form.on('checkbox(allChoose)', function (data) {
        var child = $(data.elem).parents('table').find('tbody input[type="checkbox"][name=selectedIds]');
        child.each(function (index, item) {
            item.checked = data.elem.checked;
        });
        form.render('checkbox');
    });
    //是否激活
    form.on('switch(IsActive)', function () {
        var index = layer.msg('修改中，请稍候', { icon: 16, time: false, shade: 0.8 });
        var _this = $(this);
        var id = _this.attr("data-id");
        $.ajax({
            type: 'POST',
            url: "/Users/IsActive?Id=" + id,
            dataType: 'json',
            success: function (res) {
                if (res.result.status === "ok") {
                    layer.close(index);
                    layer.msg("修改成功！");
                }
                else {
                    layer.msg('修改失败!', { icon: 2, time: 5000 });
                }
            },
            error: function (data) {
                layer.msg('请求出错!', { icon: 3, time: 5000 });
            }
        });
    });
});
var searchList = function () {
    $("#searchForm").submit();
};

var add = function () {
    layer.open({
        anim: 3,
        type: 2,
        title: '添加用户',
        shadeClose: true,
        shade: 0.8,
        area: ['600px', '600px'],
        content: '/Users/Add'
    });
};

var edit = function (id) {
    parent.layer.open({
        type: 2,
        title: '编辑用户',
        shadeClose: true,
        shade: 0.8,
        area: ['600px', '600px'],
        content: '/Home/EditId=' + id
    });
};

var deleted = function (id) {
    layer.confirm('确认要删除吗？', function () {
        $.ajax({
            type: 'POST',
            url: "/Users/Delete?Id=" + id,
            dataType: 'json',
            success: function (res) {
                if (res.result.status === "ok") {
                    layer.msg("已删除！");
                    location.reload();//刷新页面
                }
                else {
                    layer.msg('删除失败!', { icon: 2, time: 5000 });
                }
            },
            error: function (data) {
                layer.msg('请求出错!', { icon: 3, time: 5000 });
            }
        });
    });
};

var batchdeleted = function () {
    var $checkbox = $('tbody input[type="checkbox"][name=selectedIds]');
    if ($checkbox.is(":checked")) {
        layer.confirm("确认要批量删除这些数据吗？", function () {
            var formData = $("#formList").serializeArray();
            $.ajax({
                url: "/Users/BatchDelete",
                type: "post",
                data: formData,
                dataType: "json",
                success: function (res) {
                    if (res.result.status === "ok") {
                        layer.msg('已删除!');
                        location.reload();//刷新页面
                    }
                    else {
                        layer.msg('删除失败!', { icon: 2, time: 5000 });
                    }
                },
                error: function () {
                    layer.msg('请求出错!', { icon: 3, time: 5000 });
                }
            });
        });
    }
    else {
        layer.msg("未选择删除项");
    }
};



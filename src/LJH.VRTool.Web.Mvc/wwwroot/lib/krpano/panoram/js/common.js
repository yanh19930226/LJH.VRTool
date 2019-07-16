/*
 * 自定义js
 */
function print_array(arr) {
    for (var key in arr) {
        if (typeof (arr[key]) == 'array' || typeof (arr[key]) == 'object') { //递归调用    
            print_array(arr[key]);
        } else {
            document.write(key + ' = ' + arr[key] + '<br>');
            //alert(key + ' = ' + arr[key]);
        }
    }
}
//生成文件名称
function generic_name() {
    var $chars = 'abcdefghijklmnopqrstwxyz0123456789';
    var maxPos = $chars.length;
    var pwd = '';
    for (i = 0; i < 3; i++) {
        pwd += $chars.charAt(Math.floor(Math.random() * maxPos));
    }
    return new Date().getTime() + pwd;
}
/*弹框操作*/
function action_do(title, url, w, h) {
    if (!h) {
      var h = 600;
    }
    layer_show(title, url, w, h);
}

function selecttime(flag) {
    if (flag == 1) {
        var endTime = $("#countTimeend").val();
        if (endTime != "") {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd HH:mm:ss',
                maxDate: endTime
            })
        } else {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd HH:mm:ss'
            })
        }
    } else {
        var startTime = $("#countTimestart").val();
        if (startTime != "") {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd HH:mm:ss',
                minDate: startTime
            })
        } else {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd HH:mm:ss'
            })
        }
    }
}
function selecttime2(flag) {
    if (flag == 1) {
        var endTime = $("#countTimeend").val();
        if (endTime != "") {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd',
                maxDate: endTime
            });
        } else {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd'
            });
        }
    } else {
        var startTime = $("#countTimestart").val();
        if (startTime != "") {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd',
                minDate: startTime
            });
        } else {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd'
            });
        }
    }
}
/**
 * riQi(
 * 时间戳转日期
 * @return mixed 
 */
function riQi(sj, is_full_digit = true) {
    var now = new Date(sj * 1000);
    var year = now.getFullYear();
    var month = now.getMonth() + 1;

    var date = now.getDate();
    var hour = now.getHours();
    var minute = now.getMinutes();
    var second = now.getSeconds();

    if (is_full_digit) {
        if (month < 10) {
            month = '0' + month
        }
        if (date < 10) {
            date = '0' + date
        }
        if (hour < 10) {
            hour = '0' + hour
        }
        if (minute < 10) {
            minute = '0' + minute
        }
        if (second < 10) {
            second = '0' + second
        }
    }
    return year + "-" + month + "-" + date + "   " + hour + ":" + minute + ":" + second;

}

function toPercent(point) {
    var str = Number(point * 100).toFixed(2);
    str += "%";
    return str;
}


function compareDate(s1, s2) {
    return ((new Date(s1.replace(/-/g, "\/"))) > (new Date(s2.replace(/-/g, "\/"))));
}


function isURL(str_url) {
    var strRegex = '^((https|http|ftp|rtsp|mms)?://)+' +
            '?(([0-9a-z_!~*\'().&=+$%-]+: )?[0-9a-z_!~*\'().&=+$%-]+@)?' //ftp的user@ 
            +
            '(([0-9]{1,3}.){3}[0-9]{1,3}' // IP形式的URL- 199.194.52.184 
            +
            '|' // 允许IP和DOMAIN（域名） 
            +
            '([0-9a-z_!~*\'()-]+.)*' // 域名- www. 
            +
            '([0-9a-z][0-9a-z-]{0,61})?[0-9a-z].' // 二级域名 
            +
            '[a-z]{2,6})' // first level domain- .com or .museum 
            +
            '(:[0-9]{1,4})?' // 端口- :80 
            +
            '((/?)|' // a slash isn't required if there is no file name 
            +
            '(/[0-9a-z_!~*\'().;?:@&=+$,%#-]+)+/?)$';
    var re = new RegExp(strRegex);
    //re.test() 
    if (re.test(str_url)) {
        return (true);
    } else {
        return (false);
    }
}

function materialId2ImagePath(id) {
    if (typeof (localData) != 'undefined') {
        path = localData.get('mim_' + id, null, 'o')
        if (path) {
            return path
        }
    }
    $.ajax({
        url: '/home/panovapi/getImage/id/' + id,
        success: function (result) {
            localData.set('mim_' + id, result, 'o');
            materialId2ImagePath(id)
        }
    })
}

var setTableField = {
    submit: function (table, id, field, value, callback = null, ex_param = null, key = null) {

        if (table == null) {
            table = this.table
        }

        if (id == null) {
            id = this.id
        }

        if (field == null) {
            field = this.field
        }

        if (value == null) {
            value = this.value
        }

        if (key == null) {
            key = this.key
        }

        if (callback == null) {
            if (typeof (this.callback) == 'undefined') {
                callback = function (result) {
                    if (typeof (layer) != 'undefined') {
                        if (result.error) {
                            layer.msg(result.msg)
                        } else {
                            layer.msg('修改成功')
                        }
                    }
                }
            } else {
                callback = this.callback
            }
        }

        if (ex_param == null) {
            ex_param = this.ex_param
        }


        $.ajax({
            url: '/pano/panoeditmore/settablefield',
            data: {
                table: table,
                id: id,
                col: field,
                val: value,
                key: key
            },
            type: 'post',
            success: function (result) {
                if (typeof (callback) == 'function') {
                    callback(result, ex_param);
                }
            }
        })
    }
}

var setSwitch = {
    init: function () {
        if (typeof (id) != 'undefined') {
            delete(this.id)
        }

        if (typeof (field) != 'undefined') {
            delete(this.field)
        }

        if (typeof (value) != 'undefined') {
            delete(this.value)
        }

        if (typeof (msg) != 'undefined') {
            delete(this.msg)
        }

        if (typeof (callback) != 'undefined') {
            delete(this.callback)
        }

        if (typeof (ex_param) != 'undefined') {
            delete(this.ex_param)
        }
    },

    submit: function (id = null, field = null, value = null, callback = null, ex_param = null, msg = null) {

        if (id == null) {
            id = this.id
        }

        if (field == null) {
            field = this.field
        }

        if (value == null) {
            value = this.value
        }

        if (msg == null) {
            msg = this.msg
        }

        if (callback == null) {
            if (typeof (this.callback) == 'undefined') {
                callback = function (result) {
                    if (typeof (layer) != 'undefined') {
                        if (result.error) {
                            layer.msg(result.msg)
                        } else {
                            if (msg) {
                                layer.msg(msg)
                            } else {
                                layer.msg('修改成功')
                            }
                        }
                    }
                }
            } else {
                callback = this.callback
            }
        }

        if (ex_param == null) {
            ex_param = this.ex_param
        }


        $.ajax({
            url: '/pano/panoeditmore/setSwitch',
            data: {
                id: id,
                col: field,
                val: value
            },
            type: 'post',
            success: function (result) {
                if (typeof (callback) == 'function') {
                    callback(result, ex_param);
                }
            }
        })
        this.init();
    },

};


function editorInsertMaterial(option_type,editor) {
    var html = '';
    switch (option_type) {
        case 'vr':
            $("#choose_material_id").val(0);
            layer.open({
                type: 2,
                title: '选择VR全景',
                area: ['1000px', '600px'],
                content: '/pano/zuopin/select',
                end:function(){
                    var targets = $("#choose_material_id").val();
                    if(targets != 0){
                        chooseCallBack(targets)
                    }
                }
            })

            window.chooseCallBack = function (id) {
                id = id.split(',')

                
                html = '<p style="text-align:center">'+
                '<iframe  frameborder="0" width="100%" height="440px" scrolling="no" src="/t/'+id[0]+'"></iframe>'
                '</p><p><br></p>'

                editor.execCommand('inserthtml', html);
            }
            
            break;
        case 'img':
            $("#choose_material_id").val(0);
            layer.open({
                type: 2,
                title: '选择图片',
                area: ['1000px', '600px'],
                content: '/pano/material/images_select',
                end:function(){
                    var targets = $("#choose_material_id").val();
                    if(targets != 0){
                        chooseCallBack(targets)
                    }
                }
            })

            window.chooseCallBack = function (id) {
                id = id.split(',')

                $.get('/api/get/material/id/'+id[0],function(res){
                    
                    html = '<p><img style="max-width:100%;display:block;margin:0 auto;" src="'+res.source_file_txt+'?imageView2/0/w/1000" title="'+res.name+'" alt="'+res.name+'"></p><p></p>'
                    
                    editor.execCommand('inserthtml', html);
                })
            }
            
            break;
        case 'imgs':
            $("#choose_material_id").val(0);
            layer.open({
                type: 2,
                title: '选择图片',
                area: ['80%', '80%'],
                content: '/pano/material/images_select?selmore=1',
                end:function(){
                    var targets = $("#choose_material_id").val();
                    if(targets != 0){
                        // chooseCallBack(targets)
                    }
                }
            })

            window.chooseCallBack = function (id) {
                id = id.split(',')

                id.forEach(function(e){
                    $.ajax({
                        url:'/api/get/material/id/'+e,
                        type:'get',
                        async:false,
                        success:function(res){
                            html = '<p><img style="max-width:100%;display:block;margin:0 auto;" src="'+res.source_file_txt+'?imageView2/0/w/1000" title="'+res.name+'" alt="'+res.name+'"></p><p></p>'
                        
                            editor.execCommand('inserthtml', html);
                        }
                    })
                })
            }
            
            break;
            case 'music':
                $("#choose_material_id").val(0);
                layer.open({
                    type: 2,
                    title: '选择音乐',
                    area: ['1000px', '600px'],
                    content: '/pano/material/audio_select',
                    end:function(){
                        var targets = $("#choose_material_id").val();
                        if(targets != 0){
                            chooseCallBack(targets)
                        }
                    }
                })
    
                window.chooseCallBack = function (id) {
                    id = id.split(',')
                    // $.get('/api/get/material/id/'+id[0],function(res){
                    
                    //     // html = '<p><img style="max-width:100%;display:block;margin:0 auto;" src="'+res.source_file_txt+'?imageView2/0/w/600" title="'+res.name+'" alt="'+res.name+'"></p><p></p>'
                        
                    //     // editor.execCommand('inserthtml', html);

                    // })
                    html = '<p style="text-align:center">'+
                    '<iframe  frameborder="0" width="100%" height="60px" scrolling="no" src="/home/player/audio/id/'+id[0]+'"></iframe>'
                    '</p><p><br></p>'

                    editor.execCommand('inserthtml', html);
                }
            
            break;
        case 'video':
            
            $("#choose_material_id").val(0);
            layer.open({
                type: 2,
                title: '选择视频',
                area: ['1000px', '600px'],
                content: '/pano/material/video_select',
                end:function(){
                    var targets = $("#choose_material_id").val();
                    if(targets != 0){
                        chooseCallBack(targets)
                    }
                }
            })

            window.chooseCallBack = function (id) {
                id = id.split(',')
                
                html = '<p style="text-align:center">'+
                '<iframe  frameborder="0" width="100%" height="280px" scrolling="no" src="/home/player/video/id/'+id[0]+'"></iframe>'
                '</p><p><br></p>'

                editor.execCommand('inserthtml', html);
            }
            
            break;
        case 'photos':
            $("#choose_material_id").val(0);
            layer.open({
                type: 2,
                title: '选择相册',
                area: ['1000px', '600px'],
                content: '/pano/material/ebook_select',
                end:function(){
                    var targets = $("#choose_material_id").val();
                    if(targets != 0){
                        chooseCallBack(targets)
                    }
                }
            })

            window.chooseCallBack = function (id) {
                id = id.split(',')

                
                html = '<p style="text-align:center">'+
                '<iframe  frameborder="0" width="100%" height="340px" scrolling="no" src="/home/show/image.html?id='+id[0]+'"></iframe>'
                '</p><p><br></p>'

                editor.execCommand('inserthtml', html);
            }
            
            break;
        case 'threed':
            $("#choose_material_id").val(0);
            layer.open({
                type: 2,
                title: '选择3D环物',
                area: ['1000px', '600px'],
                content: '/pano/material/threed_select',
                end:function(){
                    var targets = $("#choose_material_id").val();
                    if(targets != 0){
                        chooseCallBack(targets)
                    }
                }
            })

            window.chooseCallBack = function (id) {
                id = id.split(',')

                
                html = '<p style="text-align:center">'+
                '<iframe  frameborder="0" width="100%" height="240px" scrolling="no" src="/home/material/threed.html?id='+id[0]+'"></iframe>'
                '</p><p><br></p>'

                editor.execCommand('inserthtml', html);
            }
            
            break;
        case 'button':

            var index = layer.open({
                type:1,
                title:'添加按钮',
                area: ['300px', '200px'],
                content:'<div class="add-button" style="margin:0 auto;text-align:center">'+
                '<div style="line-height:200%" >输入名称:<input type="text" id="button-name"></div>'+
                '<div style="line-height:200%">输入链接:<input type="text" id="button-url"></div>'+
                '<div><button id="confirm-button">确定</button></div>'+
                '</div><p></p>',

            })

            $('#confirm-button').click(function(){
                name = $('#button-name').val();
                url = $('#button-url').val();
                html = '<span style="width: 80px;display: block;background: red;border-radius: 30px;padding: 20px;color: #fff;text-align: center;margin: 0 auto;" onclick="openLink(\''+url+'\')">'+name+'</span>'
                console.log(html);
                
                editor.execCommand('inserthtml', html);
                layer.close(index)
            })

            break;
        default:
            break;
    }
}



/**
 * 判断是否是微信浏览器的函数
 * version {bool} 是否返回版本号
 */
function isWeChat(version) {
    var ua = window.navigator.userAgent.toLowerCase();
    if (ua.match(/MicroMessenger/i) == 'micromessenger') {
        if (version) {
            var wechatInfo = navigator.userAgent.match(/MicroMessenger\/([\d\.]+)/i);
            return wechatInfo[1];
        } else {
            return true;
        }
    } else {
        return false;
    }
}
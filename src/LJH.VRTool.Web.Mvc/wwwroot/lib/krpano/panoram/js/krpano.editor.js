var krpano = document.getElementById("krpanoSWFObject");
var sceneList = [];
var toAddHotSpot = {};
var currentSceneIndex = 0;
var movingSpot = {};
//视角拖动模块变量
var pbX = 0;
var moveIntervalId;
var canDownMove = false;
var canLeftMove = false;
var canRightMove = false;
var canShowLeft = true;

$(function () {
    //右侧功能选择
    $(".J-tool-btn").click(function () {
        $(".J-tool-btn").each(function () {
            $(this).removeClass("btn-blue");
            $("[name=" + $(this).attr("data-target") + "]").hide();
        });
        $(this).addClass("btn-blue");
        $("[name=" + $(this).attr("data-target") + "]").show();
        window.clearInterval(moveIntervalId);
        $(".add-hot-pot").hide();
    });

    //视角拖动条
    $(".triangle-down").mouseup(function () {
        canDownMove = false;
    }).mouseout(function () {
        canDownMove = false;
    });
    $(".triangle-up-left").mouseup(function () {
        canLeftMove = false;
    }).mouseout(function () {
        canLeftMove = false;
    });
    $(".triangle-up-right").mouseup(function () {
        canRightMove = false;
    }).mouseout(function () {
        canRightMove = false;
    });

    //添加热点模块
    $(".hot-style").click(function () {
        toAddHotSpot.style = $(this).attr("name");
        $(".hot-style").removeClass("hot-style-on");
        $(this).addClass("hot-style-on");
        $("#material_id").val($(this).attr("material_id"));
    });
    initSceneGroupContainerHeight();
    //可视化编辑 左侧菜单控制
    /*
     $(this).mousemove(function () {
     if (canShowLeft) {
     if (krpano.get("mouse.x") <= 300) {
     $(".left-column").css("-webkit-animation", "showLeft 0.8s infinite").css("-webkit-animation-iteration-count", "1")
     .css("left", "0");
     } else {
     $(".left-column").css("-webkit-animation", "hideLeft 0.8s infinite").css("-webkit-animation-iteration-count", "1")
     .css("left", "-250px");
     }
     }
     });
     */

    $(".my-open-out").click(function () {
        if (toAddHotSpot.dive) {
            toAddHotSpot.dive = false;
            $(this).css("background", "rgba(255,255,255,0.4)");
            $(".my-open-in").removeClass("my-open-in-open").addClass("my-open-in-close");
        } else {
            toAddHotSpot.dive = true;
            $(this).css("background", "#00a3d8");
            $(".my-open-in").removeClass("my-open-in-close").addClass("my-open-in-open");
        }
    });
});

//noinspection JSUnusedGlobalSymbols
function onready(first = 1) {
    //隐藏下方自带控制条
    krpano.set("layer[skin_control_bar].visible", false);
    krpano.set("layer[skin_splitter_bottom].visible", false);
    krpano.set("layer[skin_scroll_window].visible", false);
    //初始化场景选择列表
    var sceneListHTML;
    var dataSceneList = {list: []};
    krpano.get("scene").getArray().forEach(function (scene) {
        dataSceneList.list.push({name: scene.name, title: scene.title, view_id: scene.view_id, index: scene.index, thumburl: scene.thumburl});
    });
    //sceneListHTML = template('tplSceneList', dataSceneList);
    //document.getElementById('sceneList').innerHTML = sceneListHTML;
    //初始化选中以及首场景
    $("li[name=" + krpano.get("startscene") + "] .circle").css("background-color", "#FF9800");
    $("li[name=" + krpano.get("xml.scene") + "]").addClass("li-scene-hover");
    currentSceneIndex = krpano.get("scene").getItem(krpano.get("xml.scene")).index;
    //热点列表模块
    var hotSpotHTML;
    var dataHotSpotList = {list: []};
    //覆盖原热点选中事件,添加热点点击移动事件
    krpano.get("hotspot").getArray().forEach(function (oldHotSpot) {
        if (oldHotSpot.name !== 'vr_cursor' && oldHotSpot.name !== 'webvr_prev_scene'
                && oldHotSpot.name !== 'webvr_next_scene') {
            dataHotSpotList.list.push(oldHotSpot);
            hotSpotInitEvent(oldHotSpot.name);
        }
    });
    //右侧热点列表
    hotSpotHTML = template('tplHotSpotList', dataHotSpotList);
    document.getElementById('hotSpotList').innerHTML = hotSpotHTML;

    //绑定热点列表滑过事件
    bind_hotSpotList_mouse();
    bind_hotSpotList_edit();


    //右侧数值初始化
    $("#waitTime").val(krpano.get("autorotate.waittime"));
    if (krpano.get("autorotate.enabled")) {
        $("#autoSpin").prop("checked", true);
        $("#waitTimeInput").show();
    } else {
        $("#autoSpin").prop("checked", false);
        $("#waitTimeInput").hide();
    }

    $("#initFov").val(krpano.get("view.fov").toFixed(0));
    $("#initFovMax").val(krpano.get("view.fovmax").toFixed(0));
    $("#initFovMin").val(krpano.get("view.fovmin").toFixed(0));
    //非第一次操作即切换场景时赋值 初始加载默认赋值第一个scene
    if (first == 0) {
        $("#initVlookatMax").val(krpano.get("view.vlookatmax").toFixed(0));
        $("#initVlookatMin").val(krpano.get("view.vlookatmin").toFixed(0));
    }
    updatePbLine();
    initPbLineEvent();
    //初始化提交数据
    if (!sceneList.length) {
        krpano.get("scene").getArray().forEach(function (scene) {
            var sceneObj = {};
            sceneObj.index = scene.index;
            sceneObj.name = scene.name;
            sceneObj.view_id = scene.view_id;
            sceneObj.vlookatmax = scene.s_vlookatmax;
            sceneObj.vlookatmin = scene.s_vlookatmin;
            if (scene.name == krpano.get("startscene")) {
                sceneObj.welcomeFlag = true;
            }
            sceneList.push(sceneObj);
        });
}
//$("span[data-target=#toolHot]").click();
}
//场景切换，重命名模块
function changeScene(index) {
    //加载到选中的场景
    krpano.call("loadscene(" + krpano.get("scene").getItem(index).name + ")");
    //设置加载场景编号
    var view_id = krpano.get("scene").getItem(index).view_id;
    $("#view_id").val(view_id);

    //点了哪个场景操作哪个场景的遮罩
    update_zhezhao(view_id);
    update_qianru(view_id);
    //当前存储对象展示
    var currentScene = sceneList[index];
    //console.log(currentScene);
    if (currentScene.initH) {
        krpano.set("view.hlookat", currentScene.initH);
    }
    if (currentScene.initV) {
        krpano.set("view.vlookat", currentScene.initV);
    }
    if (currentScene.fov) {
        krpano.set("view.fov", currentScene.fov);
    }
    if (currentScene.fovmax) {
        krpano.set("view.fovmax", currentScene.fovmax);
    }
    if (currentScene.fovmin) {
        krpano.set("view.fovmin", currentScene.fovmin);
    }
    if (currentScene.vlookatmin) {
        krpano.set("view.vlookatmin", currentScene.vlookatmin);
    }
    if (currentScene.vlookatmax) {
        krpano.set("view.vlookatmax", currentScene.vlookatmax);
    }
    if (currentScene.autorotate) {
        krpano.set("autorotate.enabled", currentScene.autorotate.enabled);
        krpano.set("autorotate.waittime", currentScene.autorotate.waitTime);
    }
    /*
     if (currentScene.hotSpots) {        
     krpano.get("hotspot").getArray().forEach(function (everySpot) {
     if (everySpot.name !== "vr_cursor" && everySpot.name !== 'webvr_prev_scene'
     && everySpot.name !== 'webvr_next_scene') {
     console.log(everySpot.name + '<br>');
     krpano.call("removehotspot(" + everySpot.name + ")");
     }
     });
     currentScene.hotSpots.forEach(function (everySpot) {
     console.log(everySpot);
     krpano.call("addhotspot(" + everySpot.name + ");");
     krpano.set("hotspot[" + everySpot.name + "].ath", everySpot.ath);
     krpano.set("hotspot[" + everySpot.name + "].atv", everySpot.atv);
     krpano.set("hotspot[" + everySpot.name + "].scale", 0.5);
     krpano.set("hotspot[" + everySpot.name + "].title", everySpot.title);
     krpano.set("hotspot[" + everySpot.name + "].linkedscene", everySpot.linkedscene);
     krpano.set("hotspot[" + everySpot.name + "].name", everySpot.name);
     krpano.set("hotspot[" + everySpot.name + "].style", everySpot.style);
     krpano.set("hotspot[" + everySpot.name + "].image_url", everySpot.image_url);
     krpano.set("hotspot[" + everySpot.name + "].url", everySpot.url);
     krpano.set("hotspot[" + everySpot.name + "].hotspot_type", everySpot.hotspot_type);
     krpano.set("hotspot[" + everySpot.name + "].id", everySpot.id);
     krpano.set("hotspot[" + everySpot.name + "].dive", everySpot.dive);
     krpano.set("hotspot[" + everySpot.name + "].show_txt", everySpot.show_txt);
     krpano.set("hotspot[" + everySpot.name + "].text", everySpot.text);
     if (everySpot.htype == 1) {
     krpano.set("hotspot[" + everySpot.name + "].width", 100);
     krpano.set("hotspot[" + everySpot.name + "].height", 100);
     krpano.set("hotspot[" + everySpot.name + "].edge", 'top');
     krpano.set("hotspot[" + everySpot.name + "].htype", 1);
     krpano.set("hotspot[" + everySpot.name + "].onloaded", "add_all_the_time_tooltip();");
     } else {
     krpano.set("hotspot[" + everySpot.name + "].framewidth", 128);
     krpano.set("hotspot[" + everySpot.name + "].frameheight", 128);
     krpano.set("hotspot[" + everySpot.name + "].frame", 0);
     krpano.set("hotspot[" + everySpot.name + "].lastframe", 9);
     krpano.set("hotspot[" + everySpot.name + "].crop", "0|0|128|128");
     krpano.set("hotspot[" + everySpot.name + "].htype", 2);
     krpano.set("hotspot[" + everySpot.name + "].onloaded", "add_all_the_time_tooltip();hotspot_animate();");
     }
     });
     }
     */
    $('.li-scene-hover').removeClass('li-scene-hover');
    //$("li[name=" + krpano.get("xml.scene") + "]").addClass("li-scene-hover");
    $("li[view_id=" + view_id + "]").addClass("li-scene-hover");
    onready(0);
    hotspot_type_click($("#hotspot_type_val").val());
    //点击切换场景 获取嵌入内容
    get_qianru_data(view_id);
}
//场景切换，重命名模块-------弃用---------
function changeScene2(index) {
    //加载到选中的场景
    krpano.call("loadscene(" + krpano.get("scene").getItem(index).name + ")");
    //设置加载场景编号
    var view_id = krpano.get("scene").getItem(index).view_id;
    $("#view_id").val(view_id);
    //console.log(index);
    //console.log(sceneList);
    //当前存储对象展示
    var currentScene = sceneList[index];
    if (currentScene.initH) {
        krpano.set("view.hlookat", currentScene.initH);
    }
    if (currentScene.initV) {
        krpano.set("view.vlookat", currentScene.initV);
    }
    if (currentScene.fov) {
        krpano.set("view.fov", currentScene.fov);
    }
    if (currentScene.fovmax) {
        krpano.set("view.fovmax", currentScene.fovmax);
    }
    if (currentScene.fovmin) {
        krpano.set("view.fovmin", currentScene.fovmin);
    }
    if (currentScene.vlookatmin) {
        krpano.set("view.vlookatmin", currentScene.vlookatmin);
    }
    if (currentScene.vlookatmax) {
        krpano.set("view.vlookatmax", currentScene.vlookatmax);
    }
    if (currentScene.autorotate) {
        krpano.set("autorotate.enabled", currentScene.autorotate.enabled);
        krpano.set("autorotate.waittime", currentScene.autorotate.waitTime);
    }
    if (currentScene.hotSpots) {
        krpano.get("hotspot").getArray().forEach(function (everySpot) {
            if (everySpot.name !== "vr_cursor" && everySpot.name !== 'webvr_prev_scene'
                    && everySpot.name !== 'webvr_next_scene') {
                krpano.call("removehotspot(" + everySpot.name + ")");
            }
        });
        currentScene.hotSpots.forEach(function (everySpot) {
            krpano.call("addhotspot(" + everySpot.name + ");");
            krpano.set("hotspot[" + everySpot.name + "].ath", everySpot.ath);
            krpano.set("hotspot[" + everySpot.name + "].atv", everySpot.atv);
            krpano.set("hotspot[" + everySpot.name + "].title", everySpot.title);
            krpano.set("hotspot[" + everySpot.name + "].linkedscene", everySpot.linkedscene);
            krpano.set("hotspot[" + everySpot.name + "].dive", everySpot.dive);
            krpano.get("hotspot[" + everySpot.name + "]").loadstyle(everySpot.style);
        });
    }
    $('.li-scene-hover').removeClass('li-scene-hover');
    //$("li[name=" + krpano.get("xml.scene") + "]").addClass("li-scene-hover");
    $("li[key=" + index + "]").addClass("li-scene-hover");
    $(".circle").css("background-color", "#292827");
    sceneList.forEach(function (scene) {
        if (scene.welcomeFlag) {
            $(".circle:eq(" + scene.index + ")").css("background-color", "#FF9800");
            krpano.set("startscene", scene.name);
        }
    });
    onready(0);
}

function rename(prevButton) {
    prevButton.prev().hide();
    prevButton.attr("type", "text");
    var focusValue = prevButton.val();
    prevButton.val("");
    prevButton.focus();
    prevButton.val(focusValue);
}
//不用了 换个方式
function doRename(thisInput) {
    var nameIndex = thisInput.parent().attr('key');
    var sceneName = thisInput.parent().attr('name');
    var view_id = thisInput.parent().attr('view_id');
    var newName = thisInput.val();
    var oldName = krpano.get("scene").getItem(nameIndex).name;
    if (newName !== '') {
        sceneList[nameIndex].title = newName;
        krpano.call("set(scene[" + sceneName + "].title," + newName + ");");
        $.ajax({
            type: 'POST',
            url: globalConfig.set_scene_name_url,
            data: {view_id: view_id, newName: newName},
            dataType: 'json',
            success: function (result) {
                if (result.code == 0) {
                    layer.msg(result.msg, {time: 1500});
                } else {
                    layer.msg(result.msg, {time: 3000});
                }
            },
            error: function (data) {
                //console.log(data.msg);
            },
        });
    }
    thisInput.prev().text(newName);
    thisInput.val(newName);
    thisInput.prev().show();
    thisInput.attr("type", "hidden");
}
//不用了 换个方式
function doRename2(thisInput) {
    var nameIndex = thisInput.parent().parent().attr('key');
    var sceneName = thisInput.parent().parent().attr('name');
    var view_id = thisInput.parent().parent().attr('view_id');
    //alert(nameIndex);alert(sceneName);alert(view_id);
    var gettitle = $("#title_" + view_id).html();
    layer.prompt({title: '编辑场景', formType: 0, value: gettitle, maxlength: 140}, function (text, index) {
        $.ajax({
            type: "POST",
            url: globalConfig.set_scene_name_url,
            data: {view_id: view_id, newName: text},
            dataType: 'json',
            success: function (result) {
                if (result.code == 0) {
                    layer.msg(result.msg, {time: 1000});
                    $("#title_" + view_id).html(text);
                    sceneList[nameIndex].title = text;
                    krpano.call("set(scene[" + sceneName + "].title," + text + ");");
                    layer.close(index);
                } else {
                    layer.msg(result.msg);
                }
            }
        });
    });
}
//删除场景操作
function delview(thisInput) {
    var view_id = thisInput.parent().parent().attr('view_id');
    layer.confirm('你确定要删除该场景吗？', {
        btn: ['确定', '取消'] //按钮
    }, function () {
        $.ajax({
            type: "POST",
            url: globalConfig.del_scene_url,
            data: {view_id: view_id},
            dataType: 'json',
            success: function (result) {
                if (result.code == 0) {
                    layer.msg(result.msg, {time: 1000});
                    if($('#init_type').val()==2){
                        delview_push(view_id);
                    }
                    window.location.reload();
                } else {
                    layer.msg(result.msg);
                }
            }
        });
    }, function (index) {
        layer.close(index);
    });
}
//删除场景 推送到直播， 删除直播里的场景图片
function delview_push(view_id){
    $.get('/pano/tourlive/delView/id/'+view_id, function() {});
}


function selectWelcomeScene(view_id, index) {
    $(".circle").css("background-color", "#292827");
    $("#home_" + view_id).css("background-color", "#FF9800");
    if (view_id > 0) {
        $.ajax({
            type: 'POST',
            url: globalConfig.set_first_scene_url,
            data: {view_id: view_id},
            dataType: 'json',
            success: function (result) {
                if (result.code == 0) {
                    sceneList.forEach(function (scene) {
                        scene.welcomeFlag = false;
                    });
                    sceneList[index].welcomeFlag = true;
                    layer.msg(result.msg, {time: 1500});
                } else {
                    layer.msg(result.msg, {time: 3000});
                }
            },
            error: function (data) {
                //console.log(data.msg);
            },
        });
        //$("#isEdited").text('保存*'); 即时生效
    }
}

//视角修改模块
function setAsDefaultView() {
    sceneList[currentSceneIndex].initH = krpano.get("view.hlookat");
    sceneList[currentSceneIndex].initV = krpano.get("view.vlookat");
    sceneList[currentSceneIndex].fov = krpano.get("view.fov");
    $("#v_initH").val(krpano.get("view.hlookat"));
    $("#v_initV").val(krpano.get("view.vlookat"));
    $("#initFov").val(krpano.get("view.fov").toFixed(0));
    updatePbLine();
    var sceneName = krpano.get('xml.scene');
    $.ajax({
        type: 'POST',
        url: globalConfig.save_default_view_url,
        data: {sceneName: sceneName, hlookat: krpano.get("view.hlookat"), vlookat: krpano.get("view.vlookat"), fov: krpano.get("view.fov").toFixed(0)},
        success: function (result) {
            if (result.code == 0) {
                layer.msg(result.msg, {time: 1200});
            } else {
                layer.msg(result.msg, {time: 3000});
            }
        },
        error: function () {
            layer.msg('系统错误', {time: 3000});
        }
    });

    //layer.msg("设置成功！保存后生效", {time: 1200});
    //$("#isEdited").text('保存*');
}

function autoSpinClick() {
    var isChecked = $("#autoSpin").is(":checked");
    if (isChecked) {
        $("#waitTimeInput").show();
    } else {
        $("#waitTimeInput").hide();
    }
    krpano.set("autorotate.enabled", isChecked);
    krpano.set("autorotate.waittime", $("#waitTime").val());
    krpano.get("scene").getArray().forEach(function (scene) {
        sceneList[scene.index].autorotate = {enabled: $("#autoSpin").is(":checked"), waitTime: $("#waitTime").val()};
    });
    $("#isEdited").text('保存*');
}

function updateFov() {
    var fov = $("#initFov").val();
    var fovMax = $("#initFovMax").val();
    var fovMin = $("#initFovMin").val();
    if (fov == "" || Number(fov) > 180 || Number(fov) < 0) {
        $("#initFov").val(krpano.get("view.fov").toFixed(0));
        return
    }
    if (fovMax == "" || Number(fovMax) > 180 || Number(fovMax) < 0) {
        $("#initFovMax").val(krpano.get("view.fovmax").toFixed(0));
        return
    }
    if (fovMin == "" || Number(fovMin) > 180 || Number(fovMin) < 0) {
        $("#initFovMin").val(krpano.get("view.fovmin").toFixed(0));
        return
    }
    krpano.set("view.fov", fov);
    krpano.set("view.fovmax", fovMax);
    krpano.set("view.fovmin", fovMin);
    sceneList[currentSceneIndex].fov = fov;
    sceneList[currentSceneIndex].fovmax = fovMax;
    sceneList[currentSceneIndex].fovmin = fovMin;
    updatePbLine();
    $("#isEdited").text('保存*');
}


function updateVlookat() {
    var vlookatMax = $("#initVlookatMax").val();
    var vlookatMin = $("#initVlookatMin").val();
    if (vlookatMin === "" || Number(vlookatMin) < -90) {
        $("#initVlookatMin").val(krpano.get("view.vlookatmin").toFixed(0));
        return;
    }
    if (vlookatMax === "" || Number(vlookatMax) > 90) {
        $("#initVlookatMax").val(krpano.get("view.vlookatmax").toFixed(0));
        return;
    }
    krpano.set("view.vlookatmin", vlookatMin);
    krpano.set("view.vlookatmax", vlookatMax);
    sceneList[currentSceneIndex].vlookatmin = vlookatMin;
    sceneList[currentSceneIndex].vlookatmax = vlookatMax;
    $("#isEdited").text('保存*');
}

function setForAll() {
    sceneList.forEach(function (eachScene) {
        eachScene.fov = $("#initFov").val();
        eachScene.fovmax = $("#initFovMax").val();
        eachScene.fovmin = $("#initFovMin").val();
        eachScene.initH = $("#v_initH").val();
        eachScene.initV = $("#v_initV").val();
        eachScene.vlookatmax = $("#initVlookatMax").val();
        eachScene.vlookatmin = $("#initVlookatMin").val();
    });
    $("#isEdited").text('保存');
    var postData = JSON.stringify(sceneList);
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: globalConfig.save_url,
        data: postData,
        success: function (result) {
            if (result.code == 0) {
                $("#isEdited").text('保存');
                layer.msg(result.msg, {time: 1200});
            } else {
                layer.msg(result.msg, {time: 3000});
            }
        },
        error: function () {
            layer.msg('系统错误', {time: 3000});
        }
    });
}

function updatePbLine() {
    //视角条
    var startPx = Number(krpano.get("view.fovmin")) / 0.9;
    var widthPx = Number(krpano.get("view.fovmax")) / 0.9 - startPx;
    var currPx = Number(krpano.get("view.fov")) / 0.9 - startPx - 10;
    $(".triangle-down").css("margin-left", currPx.toFixed(0) + "px");
    $(".number-pb-shown").css("left", startPx.toFixed(0) + "px").css("width", widthPx.toFixed(0) + "px").show();
}

function initPbLineEvent() {
    $(".triangle-down").unbind("mousedown").mousedown(function () {
        pbX = krpano.get("mouse.x");
        canDownMove = true;
    }).unbind("mousemove").mousemove(function () {
        if (canDownMove) {
            moveDownLine();
        }
    });
    $(".triangle-up-left").unbind("mousedown").mousedown(function () {
        pbX = krpano.get("mouse.x");
        canLeftMove = true;
    }).unbind("mousemove").mousemove(function () {
        if (canLeftMove) {
            moveLeftLine();
        }
    });
    $(".triangle-up-right").unbind("mousedown").mousedown(function () {
        pbX = krpano.get("mouse.x");
        canRightMove = true;
    }).unbind("mousemove").mousemove(function () {
        if (canRightMove) {
            moveRightLine();
        }
    });
}

//景深设置===================================================
function moveDownLine() {
    var startPx = Number(krpano.get("view.fovmin")) / 0.9;
    var widthPx = Number(krpano.get("view.fovmax")) / 0.9 - startPx;
    var leftPx = Number(krpano.get("view.fov")) / 0.9 - startPx - 10 + krpano.get("mouse.x") - pbX;
    if (leftPx + 10 < 0) {
        canDownMove = false;
        leftPx = -10;
    }
    if (leftPx + 10 > widthPx) {
        canDownMove = false;
        leftPx = widthPx - 10;
    }
    pbX = krpano.get("mouse.x");
    krpano.set("view.fov", (leftPx + 10 + startPx) * 0.9);
    $(".triangle-down").css("margin-left", leftPx.toFixed(0) + "px");
    $("#initFov").val(krpano.get("view.fov").toFixed(0));
    sceneList[currentSceneIndex].fov = krpano.get("view.fov");
    $("#isEdited").text('保存*');
}

function moveLeftLine() {
    var startPx = Number(krpano.get("view.fovmin")) / 0.9 + krpano.get("mouse.x") - pbX;
    var endPx = Number(krpano.get("view.fovmax")) / 0.9;
    if (startPx < 0) {
        startPx = 0;
        canLeftMove = false;
    }
    if (endPx - startPx < 20) {
        startPx = endPx - 20;
        canLeftMove = false;
    }
    if (krpano.get("view.fov") < krpano.get("view.fovmin")) {
        krpano.set("view.fov", krpano.get("view.fovmin"))
    }
    pbX = krpano.get("mouse.x");
    krpano.set("view.fovmin", startPx * 0.9);
    updatePbLine();
    $("#initFovMin").val(krpano.get("view.fovmin").toFixed(0));
    sceneList[currentSceneIndex].fovmin = krpano.get("view.fovmin");
    $("#isEdited").text('保存*');
}

function moveRightLine() {
    var startPx = Number(krpano.get("view.fovmin")) / 0.9;
    var endPx = Number(krpano.get("view.fovmax")) / 0.9 + krpano.get("mouse.x") - pbX;
    if (endPx > 200) {
        endPx = 200;
        canRightMove = false;
    }
    if (endPx - startPx < 20) {
        endPx = startPx + 20;
        canRightMove = false;
    }
    if (krpano.get("view.fov") > krpano.get("view.fovmax")) {
        krpano.set("view.fov", krpano.get("view.fovmax"))
    }
    pbX = krpano.get("mouse.x");
    krpano.set("view.fovmax", endPx * 0.9);
    updatePbLine();
    $("#initFovMax").val(krpano.get("view.fovmax").toFixed(0));
    sceneList[currentSceneIndex].fovmax = krpano.get("view.fovmax");
    $("#isEdited").text('保存*');
}
//end==========================================
function compare(prop) {
    return function (obj1, obj2) {
        var val1 = obj1[prop];
        var val2 = obj2[prop];
        if (!isNaN(Number(val1)) && !isNaN(Number(val2))) {
            val1 = Number(val1);
            val2 = Number(val2);
        }

        //升序
        if (val1 < val2) {
            return -1;
        } else if (val1 > val2) {
            return 1;
        } else {
            return 0;
        }
        /*
         //降序
         if (val1 < val2) {
         return 1;
         } else if (val1 > val2) {
         return -1;
         } else {
         return 0;
         }
         */
    };
}

//提交表单修改模块
function updateHotSpotData() {
    var hotSpotHTML;
    var dataHotSpotList = {list: []};
    //修改全局变量
    var hotSpotData = [];
    krpano.get("hotspot").getArray().forEach(function (everySpot) {
        if (everySpot.name !== "vr_cursor" && everySpot.name !== 'webvr_prev_scene'
                && everySpot.name !== 'webvr_next_scene') {
            //dataHotSpotList.list.push(everySpot);
            var hotSpot = {};
            hotSpot.ath = everySpot.ath.toString();
            hotSpot.atv = everySpot.atv.toString();
            hotSpot.linkedscene = everySpot.linkedscene;
            hotSpot.name = everySpot.name;
            hotSpot.style = everySpot.style;
            hotSpot.image_url = everySpot.image_url;
            hotSpot.url = everySpot.url;
            hotSpot.hotspot_type = everySpot.hotspot_type;
            hotSpot.id = everySpot.id;
            hotSpot.title = everySpot.title;
            hotSpot.dive = everySpot.dive;
            hotSpotData.push(hotSpot);
            dataHotSpotList.list.push(hotSpot);
        }
    });
    sceneList[currentSceneIndex].hotSpots = hotSpotData;
    dataHotSpotList.list.sort(compare("id"));
    //console.log(dataHotSpotList.list);
    hotSpotHTML = template('tplHotSpotList', dataHotSpotList);
    document.getElementById('hotSpotList').innerHTML = hotSpotHTML;
    bind_hotSpotList_mouse();
    bind_hotSpotList_edit();
    //监听热点类型切换事件 显示下面相应的热点列表
    $("#hotspot_type > button").click(function () {

    });
}
//只操作热点变动情况
function updateHotSpotData2() {
    var hotSpotHTML;
    var dataHotSpotList = {list: []};
    //修改全局变量
    var hotSpotData = [];
    krpano.get("hotspot").getArray().forEach(function (everySpot) {
        if (everySpot.name !== "vr_cursor" && everySpot.name !== 'webvr_prev_scene'
                && everySpot.name !== 'webvr_next_scene') {
            //dataHotSpotList.list.push(everySpot);
            var hotSpot = {};
            hotSpot.ath = everySpot.ath.toString();
            hotSpot.atv = everySpot.atv.toString();
            hotSpot.linkedscene = everySpot.linkedscene;
            hotSpot.name = everySpot.name;
            hotSpot.style = everySpot.style;
            hotSpot.image_url = everySpot.image_url;
            hotSpot.url = everySpot.url;
            hotSpot.hotspot_type = everySpot.hotspot_type;
            hotSpot.id = everySpot.id;
            hotSpot.title = everySpot.title;
            hotSpot.htype = everySpot.htype;
            hotSpot.show_txt = everySpot.show_txt;
            hotSpot.text = everySpot.text;
            hotSpot.dive = everySpot.dive;
            hotSpotData.push(hotSpot);
            dataHotSpotList.list.push(everySpot);
        }
    });
    sceneList[currentSceneIndex].hotSpots = hotSpotData;
    //console.log(hotSpotData);
    //重置右侧热点列表 否则热点定位不准确
    dataHotSpotList.list.sort(compare("id"));
    hotSpotHTML = template('tplHotSpotList', dataHotSpotList);
    $('#hotSpotList').html(hotSpotHTML);
    bind_hotSpotList_mouse();
    bind_hotSpotList_edit();
    //console.log(hotSpotHTML);
    //alert(hotSpotHTML); 
    //document.getElementById('hotSpotList').innerHTML = hotSpotHTML;
}



function save() {
    if ($("#isEdited").text() === '保存') {
        layer.msg('保存成功', {time: 1200});
        return;
    }
    var postData = JSON.stringify(sceneList);
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: globalConfig.save_url,
        data: postData,
        success: function (result) {
            if (result.code == 0) {
                $("#isEdited").text('保存');
                layer.msg(result.msg, {time: 1200});
            } else {
                layer.msg(result.msg, {time: 3000});
            }
        },
        error: function () {
            layer.msg('系统错误', {time: 3000});
        }
    });
}
//如果是vr图片直播 则提示为发布 并调用接口
function fabu() {
    if ($("#isEdited").text() === '发布') {
        fabu_push($("#pano_id").val());
        layer.msg('发布成功', {time: 1200});
        return;
    }
    var postData = JSON.stringify(sceneList);
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: globalConfig.save_url,
        data: postData,
        success: function (result) {
            if (result.code == 0) {
                $("#isEdited").text('发布');
                fabu_push($("#pano_id").val());
                layer.msg(result.msg, {time: 1200});
            } else {
                layer.msg(result.msg, {time: 3000});
            }
        },
        error: function () {
            layer.msg('系统错误', {time: 3000});
        }
    });
}
//推送发布消息
function fabu_push(pano_id){
    $.get('/pano/tourlive/pushPano/pano_id/'+pano_id, function() {});
}

//热点移动模块
function autoMove() {
    krpano.call("screentosphere(mouse.x, mouse.y, mouseath, mouseatv);");
    krpano.set("hotspot[" + movingSpot.name + "].ath", krpano.get("mouseath") + movingSpot.athDis);
    krpano.set("hotspot[" + movingSpot.name + "].atv", krpano.get("mouseatv") + movingSpot.atvDis);
}
//tab切换

//添加热点模块
function showAddHotSpot() {
    $(".hot-style").removeClass("hot-style-on");
    $(".hot-style").first().addClass("hot-style-on");
    toAddHotSpot.style = $(".hot-style").first().attr("name");
    toAddHotSpot.dive = true;
    $(".my-open-out").css("background", "#00a3d8");
    $(".my-open-in").removeClass("my-open-in-close").addClass("my-open-in-open");
    $("#hotToolButton").hide();
    $(".add-hot-pot").show();
    $("#selectStyle").show();
    $("#goToScene").hide();
    $("#writeTitle").hide();
    $("#selectStyleTitle").addClass("progress-title-on");
    $("#goToSceneTitle").removeClass("progress-title-on");
    $("#writeTitleTitle").removeClass("progress-title-on");
}

function hideAddHotSpot() {
    window.clearInterval(moveIntervalId);
    toAddHotSpot = {};
    $(".add-hot-pot").hide();
    $("#layui-layer-shade100").hide();
    $("span[data-target=#toolHot]").click();
}

function nextToSelectTargetScene() {
    if (!toAddHotSpot.style) {
        alert("请选择热点样式");
        return
    }
    window.clearInterval(moveIntervalId);
    // 目标场景列表
    var targetSceneHTML;
    var dataTargetScene = {list: []};
    krpano.get("scene").getArray().forEach(function (scene) {
        if (scene.name !== krpano.get('xml.scene')) {
            dataTargetScene.list.push({name: scene.name, title: scene.title, view_id: scene.view_id, thumburl: scene.thumburl, index: scene.index + 1})
        }
    });
    targetSceneHTML = template('tplTargetScene', dataTargetScene);
    document.getElementById('targetScene').innerHTML = targetSceneHTML;

    $(".select-scene-div").removeClass("select-scene-div-on");
    $(".select-scene-div").first().addClass("select-scene-div-on");
    toAddHotSpot.linkedscene = $(".select-scene-div").first().attr("name");
    $(".select-scene-div").unbind("click").click(function () {
        toAddHotSpot.linkedscene = $(this).attr("name");
        $(".select-scene-div").removeClass("select-scene-div-on");
        $(this).addClass("select-scene-div-on");
    });

    $("#selectStyle").hide();
    $("#goToScene").show();
    $("#writeTitle").hide();
    $("#goToSceneTitle").addClass("progress-title-on");
    $("#selectStyleTitle").removeClass("progress-title-on");
    $("#writeTitleTitle").removeClass("progress-title-on");
}

function nextToWriteTitle() {
    if (!toAddHotSpot.linkedscene) {
        alert("请选择目标场景");
        return;
    }
    $("#addHotTitle").val(toAddHotSpot.linkedscene);
    $("#selectStyle").hide();
    $("#goToScene").hide();
    $("#writeTitle").show();
    $("#writeTitleTitle").addClass("progress-title-on");
    $("#selectStyleTitle").removeClass("progress-title-on");
    $("#goToSceneTitle").removeClass("progress-title-on");
}

function addHotSpot() {
    // 计算中间位置的球面坐标
    krpano.set("halfHeight", krpano.get("stageheight") / 2);
    krpano.set("halfWidth", krpano.get("stagewidth") / 2);
    krpano.call("screentosphere(halfWidth,halfHeight,init_h,init_v);");
    var init_h = krpano.get("init_h");
    var init_v = krpano.get("init_v");
    //添加热点
    var newHotSpotName = "spot" + new Date().getTime();
    krpano.call("addhotspot(" + newHotSpotName + ");");
    krpano.get("hotspot[" + newHotSpotName + "]").loadstyle(toAddHotSpot.style);
    krpano.set("hotspot[" + newHotSpotName + "].ath", init_h);
    krpano.set("hotspot[" + newHotSpotName + "].atv", init_v);
    krpano.set("hotspot[" + newHotSpotName + "].title", $("#addHotTitle").val());
    krpano.set("hotspot[" + newHotSpotName + "].linkedscene", toAddHotSpot.linkedscene);
    krpano.set("hotspot[" + newHotSpotName + "].dive", toAddHotSpot.dive);
    hotSpotInitEvent(newHotSpotName);
    updateHotSpotData();
    $("#isEdited").text('保存*');
    hideAddHotSpot();
    $("span[data-target=#toolHot]").click();
}

//热点选择模块
function selectHotSpot() {
    krpano.call("screentosphere(mouse.x, mouse.y, mouseath, mouseatv);");
    var nearHotSpot = {};
    krpano.get("hotspot").getArray().forEach(function (thisHotSpot) {
        var thisAthDis = krpano.get("hotspot[" + thisHotSpot.name + "]").ath - krpano.get("mouseath");
        var thisAtvDis = krpano.get("hotspot[" + thisHotSpot.name + "]").atv - krpano.get("mouseatv");
        var thisDis = Math.abs(thisAthDis) + Math.abs(thisAtvDis);
        if (!nearHotSpot.name) {
            nearHotSpot = {name: thisHotSpot.name, athDis: thisAthDis, atvDis: thisAtvDis, dis: thisDis};
        } else {
            if (thisDis < nearHotSpot.dis) {
                nearHotSpot = {name: thisHotSpot.name, athDis: thisAthDis, atvDis: thisAtvDis, dis: thisDis};
            }
        }
    });
    return nearHotSpot;
}

function hotSpotInitEvent(spotName) {
    krpano.get("hotspot[" + spotName + "]").ondown = function () {
        movingSpot = selectHotSpot();
        var intervalId = setInterval(autoMove, 1000.0 / 30.0);
        krpano.set("autoMoveIntervalId", intervalId);
        canShowLeft = false;
    };
    krpano.get("hotspot[" + spotName + "]").onup = function () {
        window.clearInterval(krpano.get("autoMoveIntervalId"));
        movingSpot = {};
        //updateHotSpotData(); //点击热点后 会重置右侧热点列表 注释这里 在写一个
        updateHotSpotData2();
        canShowLeft = true;
        $("#isEdited").text('保存*');
        save();
        //移动热点后 要定位到热点所在的8个类型菜单下
        hotspot_type_click(krpano.get("hotspot[" + spotName + "]").hotspot_type);
    };
    krpano.get("hotspot[" + spotName + "]").onclick = null;
    krpano.get("hotspot[" + spotName + "]").onover = function () {
        if (movingSpot === {}) {
            var currentSpot = selectHotSpot();
            $("[name=" + currentSpot.name + "Hover]").addClass("hot-spot-list-hover");
        }
    };
    krpano.get("hotspot[" + spotName + "]").onout = function () {
        $(".hot-spot-list").removeClass("hot-spot-list-hover");
    };
}

function removeHotSpot(name) {
    krpano.call("removehotspot(" + name + ")");
    updateHotSpotData();
    $("#isEdited").text('保存*');
}
//左侧场景菜单高度
function initSceneGroupContainerHeight() {
    var allHeight = $(window).height();
    $('.leftnav').height(allHeight);
}
//======================================热点编辑主要js操作=================================================================
//左侧 + 号添加按钮
/**
 * 添加热点操作 通过layer插件实现
 * **/
function get_hotspot_type_title(hotspot_type_val) {
    switch (hotspot_type_val) {
        case "1":
            title = "添加全景漫游热点";
            break;
        case "2":
            title = "添加超链接热点";
            break;
        case "3":
            title = "添加图集热点";
            break;
        case "4":
            title = "添加视频热点";
            break;
        case "5":
            title = "添加文本热点";
            break;
        case "6":
            title = "添加音频热点";
            break;
        case "7":
            title = "添加图文热点";
            break;
        case "8":
            title = "添加3D环物热点";
            break;
    }
    return title;
}
/**
 * 添加热点按钮
 * **/
function addhotbtn() {
    //当前要添加的热点类型
    var hotspot_type_val = $("#hotspot_type_val").val();
    var title = get_hotspot_type_title(hotspot_type_val);
    //获得当前场景中心位置
    $("#hotspot_ath").val(krpano.get("view.hlookat"));
    $("#hotspot_atv").val(krpano.get("view.vlookat"));

    //打开操作窗口
    layer.open({
        type: 2,
        area: ['60%', '80%'],
        shade: 0.8,
        move: false,
        shadeClose: false,
        title: title,
        content: globalConfig.add_hotspot_url + '?hotspot_type_val=' + hotspot_type_val
    });
}

/**
 * 修改热点操作
 * **/
function edithotbtn(hotid) {
    if (!hotid) {
        layer.msg("编辑发生错误");
        return false;
    }
    var hotspot_type_val = $("#hotspot_type_val").val();
    var title = get_hotspot_type_title(hotspot_type_val);
    //打开操作窗口
    layer.open({
        type: 2,
        area: ['60%', '80%'],
        shade: 0.8,
        move: false,
        shadeClose: false,
        title: title,
        content: globalConfig.edit_hotspot_url + '?hotspot_type_val=' + hotspot_type_val + '&hotid=' + hotid
    });
}

//添加热点操作
function add_hotspot_back(data) {
    krpano.set("halfHeight", krpano.get("stageheight") / 2);
    krpano.set("halfWidth", krpano.get("stagewidth") / 2);
    krpano.call("screentosphere(halfWidth,halfHeight,init_h,init_v);");

    //xml添加热点操作
    var init_h = krpano.get("init_h");
    var init_v = krpano.get("init_v");
    var material_id = data.material_id;
    //添加热点
    var newHotSpotName = "spot_" + data.hotid;
    krpano.call("addhotspot(" + newHotSpotName + ");");
    krpano.set("hotspot[" + newHotSpotName + "].ath", init_h);
    krpano.set("hotspot[" + newHotSpotName + "].atv", init_v);
    krpano.set("hotspot[" + newHotSpotName + "].scale", 0.5);
    krpano.set("hotspot[" + newHotSpotName + "].text", data.title);
    krpano.set("hotspot[" + newHotSpotName + "].show_txt", data.title_show);
    krpano.set("hotspot[" + newHotSpotName + "].id", data.hotid);
    krpano.set("hotspot[" + newHotSpotName + "].hotspot_type", data.hotspot_type);//热点的8个类型
    krpano.set("hotspot[" + newHotSpotName + "].image_url", data.material_info.thumb_file_txt);
    krpano.set("hotspot[" + newHotSpotName + "].title", data.title);
    krpano.set("hotspot[" + newHotSpotName + "].linkedscene", 'scene_' + data.target_viewid);
    krpano.set("hotspot[" + newHotSpotName + "].url", data.material_info.source_file_txt);
    if (data.material_info.htype == 1) {
        //静态热点        
        krpano.set("hotspot[" + newHotSpotName + "].width", 100);
        krpano.set("hotspot[" + newHotSpotName + "].height", 100);
        krpano.set("hotspot[" + newHotSpotName + "].edge", 'top');
        krpano.set("hotspot[" + newHotSpotName + "].htype", 1);
        krpano.set("hotspot[" + newHotSpotName + "].onloaded", "add_all_the_time_tooltip();");
    } else {
        krpano.set("hotspot[" + newHotSpotName + "].framewidth", 128);
        krpano.set("hotspot[" + newHotSpotName + "].frameheight", 128);
        krpano.set("hotspot[" + newHotSpotName + "].frame", 0);
        krpano.set("hotspot[" + newHotSpotName + "].lastframe", 9);
        krpano.set("hotspot[" + newHotSpotName + "].crop", "0|0|128|128");
        krpano.set("hotspot[" + newHotSpotName + "].htype", 2);
        krpano.set("hotspot[" + newHotSpotName + "].onloaded", "add_all_the_time_tooltip();hotspot_animate();");
    }
    hotSpotInitEvent(newHotSpotName);
    updateHotSpotData();
    hotspot_type_click(data.hotspot_type);
}
/**
 * 修改热点操作
 * {"code":0,"msg":"修改成功！",
 * "data":{"hotid":19,"member_id":2,"pano_id":2,"view_id":2,"title":"自定义漫游图标","title_show":1,"material_id":18,"hotspot_type":1,"hotspot_ath":"0","hotspot_atv":"0","addtime":1540608956,"deltime":0,
 * "id":3,"vrhotspot_id":19,"blend":2,"target_viewid":4,"target_hlookat":"0","target_vlookat":"0","material_info":{"id":3,"member_id":2,"name":"1540445391019s9f","type":2,"group_id":0,
 * "deltime":0,"addtime":1540445388,"material_id":18,"source_file":"1\/2\/img\/1540445391019s9f.jpg","source_file_txt":"http:\/\/panoimg.360720.com\/1\/2\/img\/1540445391019s9f.jpg",
 * "thumb_file_txt":"http:\/\/panoimg.360720.com\/1\/2\/img\/1540445391019s9f.jpg","htype":"1"}}}
 * **/
function edit_hotspot_back(data) {
    krpano.set("halfHeight", krpano.get("stageheight") / 2);
    krpano.set("halfWidth", krpano.get("stagewidth") / 2);
    krpano.call("screentosphere(halfWidth,halfHeight,init_h,init_v);");

    //xml添加热点操作
    var init_h = krpano.get("init_h");
    var init_v = krpano.get("init_v");
    var material_id = data.material_id;
    //添加热点
    var newHotSpotName = "spot_" + data.hotid;
    krpano.call("removehotspot(" + newHotSpotName + ");");
    krpano.call("addhotspot(" + newHotSpotName + ");");
    krpano.set("hotspot[" + newHotSpotName + "].ath", data.hotspot_ath);
    krpano.set("hotspot[" + newHotSpotName + "].atv", data.hotspot_atv);
    krpano.set("hotspot[" + newHotSpotName + "].scale", 0.5);
    krpano.set("hotspot[" + newHotSpotName + "].text", data.title);
    krpano.set("hotspot[" + newHotSpotName + "].show_txt", data.title_show);
    krpano.set("hotspot[" + newHotSpotName + "].id", data.hotid);
    krpano.set("hotspot[" + newHotSpotName + "].hotspot_type", data.hotspot_type);//哪种功能热点
    krpano.set("hotspot[" + newHotSpotName + "].image_url", data.material_info.thumb_file_txt);
    krpano.set("hotspot[" + newHotSpotName + "].title", data.title);
    krpano.set("hotspot[" + newHotSpotName + "].linkedscene", 'scene_' + data.target_viewid);
    krpano.set("hotspot[" + newHotSpotName + "].url", data.material_info.source_file_txt);
    if (data.material_info.htype == 1) {
        //静态热点        
        krpano.set("hotspot[" + newHotSpotName + "].width", 100);
        krpano.set("hotspot[" + newHotSpotName + "].height", 100);
        krpano.set("hotspot[" + newHotSpotName + "].edge", 'top');
        krpano.set("hotspot[" + newHotSpotName + "].htype", 1);
        krpano.set("hotspot[" + newHotSpotName + "].onloaded", "add_all_the_time_tooltip();");
    } else {
        krpano.set("hotspot[" + newHotSpotName + "].framewidth", 128);
        krpano.set("hotspot[" + newHotSpotName + "].frameheight", 128);
        krpano.set("hotspot[" + newHotSpotName + "].frame", 0);
        krpano.set("hotspot[" + newHotSpotName + "].lastframe", 9);
        krpano.set("hotspot[" + newHotSpotName + "].crop", "0|0|128|128");
        krpano.set("hotspot[" + newHotSpotName + "].htype", 2);
        krpano.set("hotspot[" + newHotSpotName + "].onloaded", "add_all_the_time_tooltip();hotspot_animate();");
    }
    hotSpotInitEvent(newHotSpotName);
    updateHotSpotData();
    hotspot_type_click(data.hotspot_type);
}

//模拟点击某个热点类型 点击效果
function hotspot_type_click(type) {
    //热点类型点击
    $("#hotspot_type > button").each(function () {
        var hotspot_type = $(this).attr('hotspot_type');
        //匹配点击不同热点类型时展示不同热点列表
        if (type == hotspot_type) {
            $(this).click();
        }
    });
}

//修改处用===========================
//设置热点图标选中状态
function set_hotstyle_on(material_id) {
    $(".hot-style").each(function () {
        var mateid = $(this).attr("material_id");
        if (material_id == mateid) {
            $(this).addClass("hot-style-on");
            $("#material_id").val(material_id);
        } else {
            $(this).removeClass("hot-style-on");
        }
    });
}
//设置场景选中状态
function set_target_viewid_on(target_viewid) {
    $(".select-scene-div").each(function () {
        var view_id = $(this).attr("view_id");
        if (target_viewid == view_id) {
            $(this).addClass("select-scene-div-on");
            $("#target_viewid").val(target_viewid);
        } else {
            $(this).removeClass("select-scene-div-on");
        }
    });
}
//设置热点标题内容状态
function set_addHotTitle_on(title) {
    $('#addHotTitle').val(title);
}
//设置过渡效果选择状态
function set_blend_on(blend) {
    $("#blend").val(blend);
}
//为右侧热点列表绑定鼠标滑过事件
function bind_hotSpotList_mouse() {
    $('#hotSpotList .edit-item').bind('mouseover', function () {
        $(this).find('.card').show();
        $(this).find('.group-item-title').css({"z-index": -1});
        $(this).siblings().find('.card').hide();
        $(this).siblings().find('.group-item-title').css({"z-index": 1});
    });
    $('#hotSpotList .edit-item').bind('mouseout', function () {
        $(this).find('.card').hide();
        $(this).find('.group-item-title').css({"z-index": 1});
    });
}
//绑定编辑、删除等事件
function bind_hotSpotList_edit() {
    //绑定热点编辑操作
    $(".group-change-icon,.group-rename").click(function () {
        var action = $(this).data('action');
        var hotid = $(this).data('hotid');
        var hotspotidx = $(this).data('hotspotidx');
        if (action == 'editIcon') {
            var hlookat = $(this).data('hlookat');
            var vlookat = $(this).data('vlookat');
            krpano.call('lookto(' + hlookat + ', ' + vlookat + ')');
        }
        if (action == 'editView') {
            edithotbtn(hotid);
        }
        return false;
    });
    //绑定删除热点操作
    $(".group-delte").click(function () {
        var hotid = $(this).data('hotid');
        var hotspot_type_val = $("#hotspot_type_val").val();
        layer.confirm('你确定要删除该热点吗？不可恢复', {
            btn: ['确定', '取消']
        }, function () {
            if (hotid <= 0) {
                layer.msg("删除失败");
                return false;
            }
            $.ajax({
                type: "POST",
                url: globalConfig.del_hotspot_url,
                data: {hotid: hotid},
                dataType: 'json',
                success: function (result) {
                    if (result.code == 0) {
                        var HotSpotName = "spot_" + hotid;
                        krpano.call("removehotspot(" + HotSpotName + ");");
                        updateHotSpotData();
                        hotspot_type_click(hotspot_type_val);
                        layer.msg(result.msg, {time: 1200});
                    }
                }
            });
        }, function () {});
    });
}
//==============================================================================================

//=========================================遮罩逻辑使用========================================
//根据场景编号从数据库读取遮罩并重新应用到场景中去
function update_zhezhao(view_id) {
    //为当前场景 添加遮罩数据
    $.ajax({
        type: 'POST',
        url: '/pano/panoedit/get_zhezhao_byid.html',
        data: {'view_id': view_id},
        success: function (result) {
            if (result.code == 0) {
                //地面遮罩
                if (result.data.bottom_material_id > 0) {
                    var bottom_mask_layer = 'bottom_mask_layer_' + view_id;
                    krpano.call("removehotspot(" + bottom_mask_layer + ")");
                    krpano.call("addhotspot(" + bottom_mask_layer + ");");
                    krpano.set("hotspot[" + bottom_mask_layer + "].scale", 0.75);
                    krpano.set("hotspot[" + bottom_mask_layer + "].ath", 0);
                    krpano.set("hotspot[" + bottom_mask_layer + "].atv", 90);
                    krpano.set("hotspot[" + bottom_mask_layer + "].rotate", 0.0);
                    krpano.set("hotspot[" + bottom_mask_layer + "].distorted", true);
                    //krpano.set("hotspot[" + bottom_mask_layer + "].enabled", false);
                    krpano.set("hotspot[" + bottom_mask_layer + "].handcursor", false);
//                        krpano.set("hotspot[" + bottom_mask_layer + "].visible", true);
                    krpano.set("hotspot[" + bottom_mask_layer + "].keep", false);
                    krpano.set("hotspot[" + bottom_mask_layer + "].url", result.data.bottom_mateinfo.source_file_txt);
                }
                //天空遮罩
                if (result.data.top_material_id > 0) {
                    var top_mask_layer = 'top_mask_layer_' + view_id;
                    krpano.call("removehotspot(" + top_mask_layer + ")");
                    krpano.call("addhotspot(" + top_mask_layer + ");");
                    krpano.set("hotspot[" + top_mask_layer + "].scale", 0.75);
                    krpano.set("hotspot[" + top_mask_layer + "].ath", 0);
                    krpano.set("hotspot[" + top_mask_layer + "].atv", -90);
                    krpano.set("hotspot[" + top_mask_layer + "].rotate", 0.0);
                    krpano.set("hotspot[" + top_mask_layer + "].distorted", true);
                    //krpano.set("hotspot[" + top_mask_layer + "].enabled", false);
                    krpano.set("hotspot[" + top_mask_layer + "].handcursor", false);
//                        krpano.set("hotspot[" + top_mask_layer + "].visible", true);
                    krpano.set("hotspot[" + top_mask_layer + "].keep", false);
                    krpano.set("hotspot[" + top_mask_layer + "].url", result.data.top_mateinfo.source_file_txt);
                }
            }
        }
    });
}
//=========================================遮罩结束=========================


//=========================================嵌入逻辑使用======================
/**
 * 嵌入创建后回调
 * id 嵌入表id
 * type 1文本 2图片 3序列帧 4视频
 * **/
function update_qianru(view_id) {
    //获取嵌入数据
    $.ajax({
        type: 'POST',
        url: '/pano/panoedit/get_qianru_data.html',
        data: {'view_id': view_id},
        success: function (result) {
            if (result.code == 0) {
                var data = result.data;
                for (var i = 0; i < data.length; i++) {
                    var type = data[i].type;
                    //嵌入文本
                    if (type == 1) {
                        var embed_name = "embed_" + data[i].id;
                        krpano.call("removehotspot(" + embed_name + ")");
                        krpano.call("addhotspot(" + embed_name + ");");
                        krpano.set("hotspot[" + embed_name + "].ath", data[i].ath);
                        krpano.set("hotspot[" + embed_name + "].atv", data[i].atv);
                        krpano.set("hotspot[" + embed_name + "].dataid", data[i].id);
                        krpano.set("hotspot[" + embed_name + "].scale", 1);
                        krpano.set("hotspot[" + embed_name + "].keep", false);
                        krpano.set("hotspot[" + embed_name + "].url", '%SWFPATH%/plugins/textfield.swf');
                        krpano.set("hotspot[" + embed_name + "].html", data[i].content);
                        krpano.set("hotspot[" + embed_name + "].backgroundcolor", '#000000');
                        krpano.set("hotspot[" + embed_name + "].backgroundalpha", 0.5);
                        krpano.set("hotspot[" + embed_name + "].roundedge", 5);
                        krpano.set("hotspot[" + embed_name + "].padding", 10);
                        krpano.set("hotspot[" + embed_name + "].multiline", true);
                        krpano.set("hotspot[" + embed_name + "].oy", -9);
                        krpano.set("hotspot[" + embed_name + "].css", 'font-family:STXihei; font-size:12px; color:#ffffff;letter-spacing:1px;textAlign:center;');
                        krpano.set("hotspot[" + embed_name + "].edge", 'bottom');
                        krpano.set("hotspot[" + embed_name + "].handcursor", true);
                        krpano.set("hotspot[" + embed_name + "].ondown", 'draghotspot()');
                        krpano.set("hotspot[" + embed_name + "].onup", "ed_hot(hot_move,embed);");
                    }
                    //图片
                    if (type == 2) {
                        var embed_name = "embed_" + data[i].id;
                        krpano.call("removehotspot(" + embed_name + ")");
                        krpano.call("addhotspot(" + embed_name + ");");
                        krpano.set("hotspot[" + embed_name + "].keep", false);
                        krpano.set("hotspot[" + embed_name + "].url", globalConfig.cdnhost + '/' + data[i].pics_first_img + '?imageView2/0/w/800/h/800');
                        krpano.set("hotspot[" + embed_name + "].scale", 0.5);
                        krpano.set("hotspot[" + embed_name + "].ath", data[i].ath);
                        krpano.set("hotspot[" + embed_name + "].atv", data[i].atv);
                        krpano.set("hotspot[" + embed_name + "].dataid", data[i].id);
                        krpano.set("hotspot[" + embed_name + "].edge", 'center');
                        krpano.set("hotspot[" + embed_name + "].distorted", true);
                        krpano.set("hotspot[" + embed_name + "].zoom", true);
                        krpano.set("hotspot[" + embed_name + "].renderer", 'css3d');
                        krpano.set("hotspot[" + embed_name + "].image_count", 1);
                        krpano.set("hotspot[" + embed_name + "].image_index", 0);
                        krpano.set("hotspot[" + embed_name + "].handcursor", true);
                        krpano.set("hotspot[" + embed_name + "].ondown", 'draghotspot();');
                        krpano.set("hotspot[" + embed_name + "].onup", "ed_hot(hot_move,embed);");
                    }
                    //视频 用图片来代表
                    if (type == 3) {
                        var embed_name = "embed_" + data[i].id;
                        krpano.call("removehotspot(" + embed_name + ")");
                        krpano.call("addhotspot(" + embed_name + ");");
                        krpano.set("hotspot[" + embed_name + "].keep", false);
                        krpano.set("hotspot[" + embed_name + "].url", globalConfig.cdnhost + '/' + data[i].posterurl + '?imageView2/0/w/800/h/800');
                        krpano.set("hotspot[" + embed_name + "].scale", 0.5);
                        krpano.set("hotspot[" + embed_name + "].ath", data[i].ath);
                        krpano.set("hotspot[" + embed_name + "].atv", data[i].atv);
                        krpano.set("hotspot[" + embed_name + "].width", data[i].video_width);
                        krpano.set("hotspot[" + embed_name + "].height", data[i].video_height);
                        krpano.set("hotspot[" + embed_name + "].dataid", data[i].id);
                        krpano.set("hotspot[" + embed_name + "].edge", 'center');
                        krpano.set("hotspot[" + embed_name + "].distorted", true);
                        krpano.set("hotspot[" + embed_name + "].zoom", true);
                        krpano.set("hotspot[" + embed_name + "].renderer", 'css3d');
                        krpano.set("hotspot[" + embed_name + "].handcursor", true);
                        krpano.set("hotspot[" + embed_name + "].ondown", 'draghotspot();');
                        krpano.set("hotspot[" + embed_name + "].onup", "ed_hot(hot_move,embed);");
                    }
                    //序列帧动画
                    if (type == 4) {
                        //gif
                        if (data[i].mateinfo.type == 1) {
                            var embed_name = "embed_" + data[i].id;
                            krpano.call("removehotspot(" + embed_name + ")");
                            krpano.call("addhotspot(" + embed_name + ");");
                            krpano.set("hotspot[" + embed_name + "].keep", false);
                            krpano.set("hotspot[" + embed_name + "].url", globalConfig.cdnhost + '/' + data[i].mateinfo.source_file);
                            krpano.set("hotspot[" + embed_name + "].scale", 0.5);
                            krpano.set("hotspot[" + embed_name + "].ath", data[i].ath);
                            krpano.set("hotspot[" + embed_name + "].atv", data[i].atv);
                            krpano.set("hotspot[" + embed_name + "].dataid", data[i].id);
                            krpano.set("hotspot[" + embed_name + "].edge", 'center');
                            krpano.set("hotspot[" + embed_name + "].renderer", 'css3d');
                            krpano.set("hotspot[" + embed_name + "].handcursor", true);
                            krpano.set("hotspot[" + embed_name + "].ondown", 'draghotspot();');
                            krpano.set("hotspot[" + embed_name + "].onup", "ed_hot(hot_move,embed);");
                        }
                        //png
                        if (data[i].mateinfo.type == 2) {
                            var embed_name = "embed_" + data[i].id;
                            krpano.call("removehotspot(" + embed_name + ")");
                            krpano.call("addhotspot(" + embed_name + ");");
                            krpano.set("hotspot[" + embed_name + "].keep", false);
                            krpano.set("hotspot[" + embed_name + "].url", globalConfig.cdnhost + '/' + data[i].mateinfo.source_file);
                            krpano.set("hotspot[" + embed_name + "].scale", 0.5);
                            krpano.set("hotspot[" + embed_name + "].ath", data[i].ath);
                            krpano.set("hotspot[" + embed_name + "].atv", data[i].atv);
                            krpano.set("hotspot[" + embed_name + "].dataid", data[i].id);
                            krpano.set("hotspot[" + embed_name + "].edge", 'center');
                            krpano.set("hotspot[" + embed_name + "].renderer", 'css3d');
                            krpano.set("hotspot[" + embed_name + "].handcursor", true);
                            krpano.set("hotspot[" + embed_name + "].ondown", 'draghotspot();');
                            krpano.set("hotspot[" + embed_name + "].onup", "ed_hot(hot_move,embed);");
                            krpano.set("hotspot[" + embed_name + "].framewidth", data[i].mateinfo.width);
                            krpano.set("hotspot[" + embed_name + "].frameheight", data[i].mateinfo.height);
                            krpano.set("hotspot[" + embed_name + "].frame", 0);
                            krpano.set("hotspot[" + embed_name + "].lastframe", 10);
                            krpano.set("hotspot[" + embed_name + "].crop", "0|0|" + data[i].mateinfo.width + "|" + data[i].mateinfo.height + "");
                            krpano.set("hotspot[" + embed_name + "].onloaded", "do_crop_animation();");
                        }
                    }
                }
                //更新嵌入列表
                get_qianru_data($('#view_id').val());
            } else {
                layer.msg('系统错误', {time: 3000});
            }
        },
        error: function () {
            layer.msg('系统错误', {time: 3000});
        }
    });
}
/**
 * 保存 移动热点的坐标
 * **/
function move_zuobiao(type, dataid, ath, atv) {
    //console.log(type);
    //console.log(name);    
    //console.log(ath);
    //console.log(atv);
    $.ajax({
        type: 'POST',
        url: '/pano/panoedit/move_zuobiao.html',
        data: {'type': type, 'dataid': dataid, 'ath': ath, 'atv': atv},
        success: function (result) {
            if (result.code == 0) {
                layer.msg(result.msg, {time: 1200});
            } else {
                layer.msg(result.msg, {time: 3000});
            }
        },
        error: function () {
            layer.msg('系统错误', {time: 3000});
        }
    });



//将鼠标坐标转换为球星图坐标   
//    var mx = krpano.get("mouse.x");
//    var my = krpano.get("mouse.y");
//    var pnt = krpano.screentosphere(mx, my);
//    var h = pnt.x;
//    var v = pnt.y;
    //console.log('x="' + mx + '" y="' + my + '" ath="' + h.toFixed(2) + '" atv="' + v.toFixed(2) + '"');

}


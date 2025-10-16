window.convertArray = (win1251Array) => {
    // JS解码器
    var win1251decoder = new TextDecoder('windows-1251');
    var bytes = new Uint8Array(win1251Array);
    var decodedArray = win1251decoder.decode(bytes);
    console.log(decodedArray);
    return decodedArray;
};

// 设置元素的文本
window.setElementText = (element, text) => {
    element.innerText = text;
    return text;
};

window.blazorJsFunctions = {
    showPrompt: function (text) {
        return prompt(text, "Type your name here");
    },
    displayWelcome: function (welcomeMessage) {
        document.getElementById('welcome').innerText = welcomeMessage;
    },
    // JS调用.NET静态方法
    returnArrayAsyncJs: function () {
        // 程序集名称、方法名或用JSInvokable为方法指定的标识符
        DotNet.invokeMethodAsync('BlazorAppOne.SSR', 'ReturnArrayAsync')
            .then(data => {
                data.push(4);
                console.log(data);
            });
    },
    // JS调用.NET实例方法
    sayHello: function (dotnetHelper) {
        return dotnetHelper.invokeMethodAsync('SayHello')
            .then(r => console.log(r));
    },
    // JS调用组件实例方法
    updateMessageCallerJS: function () {
        DotNet.invokeMethodAsync('BlazorAppOne.SSR', 'UpdateMessageCaller');
    },
    // JS调用组件实例方法
    updateMessageCallerJS1: function (dotnetHelper) {
        dotnetHelper.invokeMethodAsync('UpdateMessageCaller');
        dotnetHelper.dispose();
    },
    // 启动摄像头
    startVideo: function (videoElementId) {
        var video = document.getElementById(videoElementId);

        if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
            navigator.mediaDevices.getUserMedia({ video: true }).then(function (stream) {
                try {
                    // 新版本chrome版本不支持该用法
                    video.src = window.URL.createObjectURL(stream);
                } catch (e) {
                    video.srcObject = stream;
                }
            });
        }
    }
};
$(function () {
    var chat = $.connection.gameHub;

    chat.client.addNewMessageToPage = function (name, message) {
        $('#discussion').append('<li><strong>' + name
            + '</strong>: ' + message + '</li>');
    };

    $('#displayname').val(prompt('Enter your name:', ''));
    $('#message').focus();

    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
            chat.server.send($('#displayname').val(), $('#message').val());
        });

        $("#angel").on("click", function () {
            alert($(this).attr('value'));
            chat.server.addUnit($(this).attr('value'));
        });

        $("#archer").on("click", function () {
            chat.server.addUnit($(this).attr('value'));
        });

        $("#monster").on("click", function () {
            chat.gameplayHub.server.addUnit($(this).attr('value'));
        });
    });

    var stage = new Kinetic.Stage({
        container: 'container',
        width: 960,
        height: 540
    });
    var layer = new Kinetic.Layer();

    var imageObj = new Image();

    chat.client.addNewUnitInTheMap = function (unit) {
        imageObj.src = unit.SpriteLink;
        imageObj.onload = function () {
            var blob = new Kinetic.Sprite({
                x: 250,
                y: 40,
                image: imageObj,
                animation: 'idle',
                animations: unit.SpritePoints,
                frameRate: 7,
                frameIndex: 0
            });

            layer.add(blob);
            stage.add(layer);
            blob.start();
        }
        console.log(unit.Name);
        console.log(unit.SpritePoints["idle"]);
    }
});
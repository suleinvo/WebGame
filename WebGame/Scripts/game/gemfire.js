$(function () {
    var stage = new Kinetic.Stage({
        container: 'container',
        width: 800,
        height: 540
    });
    var layer = new Kinetic.Layer();
    var i = 0;
    var p = [{ x: 250, y: 40 }, { x: 250, y: 140 }, { x: 250, y: 240 }];
    p[0].x = 250;
    p[0].y = 40;
    p[1].x = 250;
    p[1].y = 140;
    p[2].x = 250;
    p[2].y = 240;


    stage.add(layer);
  
    var imageObj = new Image();

    var chat = $.connection.gameHub;

    chat.client.addNewMessageToPage = function (name, message) {
        $('#discussion').append('<li><strong>' + name
            + '</strong>: ' + message + '</li>');
    };

    $('#displayname').val(prompt('Enter your name:', ''));
    $('#message').focus();

    $.connection.hub.start().done(function () {
        chat.server.addUser($('#displayname').val());

        $('#sendmessage').click(function () {
            chat.server.send($('#displayname').val(), $('#message').val());
            $('#message').val('');
        });

        $("#angel").on("click", function () {
            chat.server.addUnit($(this).attr('value'));
        });

        $("#archer").on("click", function () {
            chat.server.addUnit($(this).attr('value'));
        });

        $("#monster").on("click", function () {
            chat.gameplayHub.server.addUnit($(this).attr('value'));
        });
    });

    chat.client.addNewUnitInTheMap = function(unit, player) {
        if (i < 3) {
            var image = new Image();
            image.src = unit.SpriteLink;
            image.onload = function() {
                var blob = new Kinetic.Sprite({
                    x: p[i].x,
                    y: p[i].y,
                    image: image,
                    animation: 'idle',
                    animations: unit.SpritePoints,
                    frameRate: 7,
                    frameIndex: 0,
                    id: i,
                    scaleX: player
            });
                layer.add(blob);
                blob.start();
                i++;
            }
            console.log(unit.Name);
            console.log(unit.SpritePoints["idle"]);
        }
    }
});
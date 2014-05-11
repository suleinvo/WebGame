$(function () {
    var stage = new Kinetic.Stage({
        container: 'container',
        width: 960,
        height: 540
    });
    var layer = new Kinetic.Layer();

    var imageObj = new Image();

    imageObj.onload = function () {
        var blob = new Kinetic.Sprite({
            x: 250,
            y: 40,
            image: imageObj,
            animation: 'idle',
            animations: {
                idle: [
                    // x, y, width, height (4 frames)
                    2, 2, 70, 119,
                    71, 2, 74, 119,
                    146, 2, 81, 119,
                    226, 2, 76, 119
                ],
                punch: [
                    // x, y, width, height (3 frames)
                    2, 138, 74, 122,
                    76, 138, 84, 122,
                    346, 138, 120, 122
                ]
            },
            frameRate: 7,
            frameIndex: 0
        });

        // add the shape to the layer
        layer.add(blob);

        // add the layer to the stage
        stage.add(layer);

        // start sprite animation
        blob.start();

        var frameCount = 0;

        blob.on('frameIndexChange', function () {
            if (blob.animation() === 'punch' && ++frameCount > 3) {
                blob.animation('idle');
                frameCount = 0;
            }
        });

        document.getElementById('punch').addEventListener('click', function () {
            blob.animation('punch');
        }, false);
    };

    imageObj.src = 'http://www.html5canvastutorials.com/demos/assets/blob-sprite.png';
});
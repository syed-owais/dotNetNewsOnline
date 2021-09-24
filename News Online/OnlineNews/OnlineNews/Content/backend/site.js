$(document).ready(function() {
    //$('.nav-item').find('[href="' + window.location.pathname + '"]').parent().addClass('active');
    $('.nav-item').find('[href="' + window.location.pathname + '"]').addClass('active');
})

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#image')
                .attr('src', e.target.result)
                .width(150)
                .height(150);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

function readURLPoster(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#posterimg')
                .attr('src', e.target.result)
                .width(150)
                .height(150);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

$('.selectbox').select2();

//Get thumbnail on upload video at runtime 
if (document.getElementById('video') != null) {
    function uploadVideo(obj) {
        document.getElementById('video').addEventListener('change',
            function(event) {
                if ($("#video-thumbnail").children().length > 1) {
                    $("#video-thumbnail").children()[1].remove();

                }
                var file = event.target.files[0];
                var fileReader = new FileReader();
                if (file.type.match('image')) {
                    fileReader.onload = function() {
                        var img = document.createElement('img');
                        img.src = fileReader.result;
                        document.getElementsByTagName('div')[obj].appendChild(img);
                    };
                    fileReader.readAsDataURL(file);
                } else {
                    fileReader.onload = function() {
                        var blob = new Blob([fileReader.result], { type: file.type });
                        var url = URL.createObjectURL(blob);
                        var video = document.createElement('video');
                        var timeupdate = function() {
                            if (snapImage()) {
                                video.removeEventListener('timeupdate', timeupdate);
                                video.pause();
                            }
                        };
                        video.addEventListener('loadeddata',
                            function() {
                                if (snapImage()) {
                                    video.removeEventListener('timeupdate', timeupdate);
                                }
                            });
                        var snapImage = function() {
                            var canvas = document.createElement('canvas');
                            canvas.width = video.videoWidth;
                            canvas.height = video.videoHeight;
                            canvas.getContext('2d').drawImage(video, 0, 0, canvas.width, canvas.height);
                            var image = canvas.toDataURL();
                            var success = image.length > 100000;
                            if (success) {
                                var img = document.createElement('img');
                                img.src = image;
                                document.getElementsByTagName('div')[obj].appendChild(img);
                                URL.revokeObjectURL(url);
                            }
                            return success;
                        };
                        video.addEventListener('timeupdate', timeupdate);
                        video.preload = 'metadata';
                        video.src = url;
                        // Load video in Safari / IE11
                        video.muted = true;
                        video.playsInline = true;
                        video.play();
                    };
                    fileReader.readAsArrayBuffer(file);
                }
                $("#video-thumbnail").has("img").height("150").width("150");
            });
    }
}



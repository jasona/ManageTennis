
var liObj;
var liFirstObj;
var divName;

function theRotator() {


    if ($('div#ImageRoller ul li').length) {
        liObj = $('div#ImageRoller ul li');
        divName = "ImageRoller";
    }
//    else if ($('div#pageRotator ul li').length) {
//        liObj = $('div#pageRotator ul li');
//        divName = "pageRotator";
//    }

    if ($('div#ImageRoller ul li:first').length) {
        liFirstObj = $('div#ImageRoller ul li:first');
    }
//    else if ($('div#pageRotator ul li:first').length) {
//        liFirstObj = $('div#pageRotator ul li:first');
//    }
 
    //Set the opacity of all images to 0
    liObj.css({ opacity: 0.0 });


    //Get the first image and display it (gets set to full opacity)
    liFirstObj.css({ opacity: 1.0 });

    //Call the rotator function to run the slideshow, 6000 = change to next image after 6 seconds
    setInterval('rotate()', 4000);

}



function rotate() {

    //Get the first image
    var current = ($('div#' + divName + ' ul li.show') ? $('div#' + divName + ' ul li.show') : $('div#' + divName + ' ul li:first'));

    //Get next image, when it reaches the end, rotate it back to the first image
    var next = ((current.next().length) ? ((current.next().hasClass('show')) ? $('div#' + divName + ' ul li:first') : current.next()) : $('div#' + divName + ' ul li:first'));


    //Set the fade in effect for the next image, the show class has higher z-index
    next.css({ opacity: 0.0 })
          .addClass('show')
          .animate({ opacity: 1.0 }, 1000);



    //Hide the current image
    current.animate({ opacity: 0.0 }, 1000)
          .removeClass('show');


};



$(document).ready(function () {
    //Load the slideshow
    theRotator();
    $('div.rotator').fadeIn(1000);
});




$(document).ready(function(){

	// hide on load
	$("div.hideshow03").hide();

	// hide/show on click
	$("a.hideshow03").click(function(){
		$("div.hideshow03").slideToggle("slow");
	});
	
	
	

	// success demo, just for show...
	// remove these lines
	$(".success").hide();
	$(".success").fadeIn("slow");
	$(".error").hide();
	$(".error").fadeIn("slow");
	
	
	
	$(".hideSuccess").click(function(){
		$(".success").fadeOut("slow");
	});
	$(".hideError").click(function(){
		$(".error").fadeOut("slow");
	});
	
	
});
$(function () {
	$(".signin a").click(function () {
		$(".form-signin").removeClass("deactivate");
		$(".form-signin").addClass("active-form");
		$(".form-signup").removeClass("active-form");
		$(".form-signup").addClass("deactivate");
		$(".signup").removeClass("active");
		$(".signin").addClass("active");

		

	});
	$(".signup a").click(function () {
		$(".form-signin").removeClass("active-form");
		$(".form-signin").addClass("deactivate");
		$(".form-signup").removeClass("deactivate");

		$(".form-signup").addClass("active-form");
		$(".signin").removeClass("active");
		$(".signup").addClass("active");

	});
});

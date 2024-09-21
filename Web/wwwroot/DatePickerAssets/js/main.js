$(function() {

  // rome(inline_cal, { time: false });


	rome(inline_cal_from, {time: false, inputFormat: 'MMMM DD, YYYY', dateValidator: rome.val.beforeEq(inline_cal_to)}).on('data', function (value) {
	  result_from.value = value;
	});

	rome(inline_cal_to, {time: false, inputFormat: 'MMMM DD, YYYY', dateValidator: rome.val.afterEq(inline_cal_from)}).on('data', function (value) {
	  result_to.value = value;
	});


});
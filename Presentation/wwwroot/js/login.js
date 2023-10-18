﻿
$(function () {
	'use strict';

	$('.form-control').on('input', function () {
		var $field = $(this).closest('.form-group');
		if (this.value) {
			$field.addClass('field--not-empty');
		} else {
			$field.removeClass('field--not-empty');
		}
	});

});

//$(document).ready(function () {
//	$('#forgotPasswordModal').on('shown.bs.modal', function () {
//		$('#forgotPasswordEmail').focus(); // Modal açıldığında email input'a odaklan
//	});
//});
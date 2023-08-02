//jQuery.validator.setDefaults({
//	highlight: function (element, errorClass, validClass) {
//		if (element.type === 'radio') {
//			this.findByName(element.name).addClass(errorClass).removeClass(validClass);
//		} else {
//			$(element).addClass(errorClass).removeClass(validClass);
//			$(element).closest('.form-group').removeClass('has-success').addClass('has-error');
//		}
//	},
//	unhighlight: function (element, errorClass, validClass) {
//		if (element.type === 'radio') {
//			this.findByName(element.name).removeClass(errorClass).addClass(validClass);
//		} else {
//			$(element).removeClass(errorClass).addClass(validClass);
//			$(element).closest('.form-group').removeClass('has-error').addClass('has-success');
//		}
//	}
//});
jQuery.validator.setDefaults({
	highlight: function (element, errorClass, validClass) {
		if (element.type === 'radio') {
			this.findByName(element.name).addClass(errorClass).removeClass(validClass);
		} else {
			$(element).addClass(errorClass).removeClass(validClass);
			$(element).closest('.form-group').addClass('has-error');
		}
	},
	unhighlight: function (element, errorClass, validClass) {
		if (element.type === 'radio') {
			this.findByName(element.name).removeClass(errorClass).addClass(validClass);
		} else {
			$(element).removeClass(errorClass).addClass(validClass);
			$(element).closest('.form-group').removeClass('has-error');
		}
	}
});

$(function () {
	$("span.field-validation-valid, span.field-validation-error").addClass('help-block');
	$("div.form-group").has("span.field-validation-error").addClass('has-error');
	$("div.validation-summary-errors").has("li:visible").addClass("alert alert-block alert-danger");
});


//<div class="form-group has-success">
//  <label class="control-label" for="inputSuccess"><i class="fa fa-check"></i> Input with success</label>
//  <input type="text" class="form-control" id="inputSuccess" placeholder="Enter ...">
//</div>
//<div class="form-group has-warning">
//  <label class="control-label" for="inputWarning"><i class="fa fa-bell-o"></i> Input with warning</label>
//  <input type="text" class="form-control" id="inputWarning" placeholder="Enter ...">
//</div>
//<div class="form-group has-error">
//  <label class="control-label" for="inputError"><i class="fa fa-times-circle-o"></i> Input with error</label>
//  <input type="text" class="form-control" id="inputError" placeholder="Enter ...">
//</div>
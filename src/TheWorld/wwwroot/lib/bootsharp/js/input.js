// Focus state for append/prepend inputs
jQuery('.input-group').on('focus', '.form-control', function () {
  jQuery(this).closest('.input-group, .form-group').addClass('focus');
}).on('blur', '.form-control', function () {
  jQuery(this).closest('.input-group, .form-group').removeClass('focus');
});

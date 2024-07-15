﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//limit form input to 'count' decimal
function limitDecimalPlaces(e, count) {
  if (e.target.value.indexOf(".") == -1) {
    return;
  }
  if (e.target.value.length - e.target.value.indexOf(".") > count) {
    e.target.value = parseFloat(e.target.value).toFixed(count);
  }
}


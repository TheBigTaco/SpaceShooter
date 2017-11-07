$(document).ready(function() {
  $(".menu-circle").click(function() {
    $(".menu-circle").toggleClass("x-active");
    $(".menu-game").toggle();
    $(".menu-profile").toggle();
    $(".menu-login").toggle();
    $(".menu-leaderboard").toggle();
    $(".menu-register").toggle();
    $(".search-form").fadeToggle();
  });
});

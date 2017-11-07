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

  $("#sprite-button").click(function() {
    $(".profile-sprites-hide").show();
    $(".sprites-prompt").show();
    $(".themes-prompt").hide();
    $(".scores-prompt").hide();
    $(".profile-themes-hide").hide();
    $(".profile-scores-hide").hide();
  });

  $("#theme-button").click(function() {
    $(".profile-themes-hide").show();
    $(".themes-prompt").show();
    $(".scores-prompt").hide();
    $(".sprites-prompt").hide();
    $(".profile-sprites-hide").hide();
    $(".profile-scores-hide").hide();
  });

  $("#score-button").click(function() {
    $(".profile-scores-hide").show();
    $(".scores-prompt").show();
    $(".themes-prompt").hide();
    $(".sprites-prompt").hide();
    $(".profile-sprites-hide").hide();
    $(".profile-themes-hide").hide();
  });

});

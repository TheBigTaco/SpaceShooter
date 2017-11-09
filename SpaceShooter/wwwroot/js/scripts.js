$(document).ready(function() {
  $(".change-background").click(function() {
    $("body").addClass("x-background");
  });

  $(".menu-circle").click(function() {
    $(".menu-circle").toggleClass("x-active");
    $(".menu-game").toggle();
    $(".menu-profile").toggle();
    $(".menu-main").toggle();
    $(".menu-leaderboard").toggle();
    $(".menu-search").toggle();
  });

  $(".links-register").click(function() {
    $(".links").hide();
    $(".register-form").show();
  });

  $(".links-login").click(function() {
    $(".links").hide();
    $(".login-form").show();
  });

  $("#sprite-button").click(function() {
    $(".profile-sprites-hide").show();
    $(".sprites-prompt").show();
    $(".themes-prompt").hide();
    $(".friends-prompt").hide();
    $(".profile-themes-hide").hide();
    $(".profile-friends-hide").hide();
    $("#sprite-button").addClass("profile-button-click");
    $("#theme-button").removeClass("profile-button-click");
    $("#friend-button").removeClass("profile-button-click");
    $("#sprite-button").removeClass("button-border");
    $("#theme-button").addClass("button-border");
    $("#friend-button").addClass("button-border");
  });

  $("#theme-button").click(function() {
    $(".profile-themes-hide").show();
    $(".themes-prompt").show();
    $(".friends-prompt").hide();
    $(".sprites-prompt").hide();
    $(".profile-sprites-hide").hide();
    $(".profile-friends-hide").hide();
    $("#sprite-button").removeClass("profile-button-click");
    $("#theme-button").addClass("profile-button-click");
    $("#friend-button").removeClass("profile-button-click");
    $("#sprite-button").addClass("button-border");
    $("#theme-button").removeClass("button-border");
    $("#friend-button").addClass("button-border");  });

  $("#friend-button").click(function() {
    $(".profile-friends-hide").show();
    $(".friends-prompt").show();
    $(".themes-prompt").hide();
    $(".sprites-prompt").hide();
    $(".profile-sprites-hide").hide();
    $(".profile-themes-hide").hide();
    $("#sprite-button").removeClass("profile-button-click");
    $("#theme-button").removeClass("profile-button-click");
    $("#friend-button").addClass("profile-button-click");
    $("#sprite-button").addClass("button-border");
    $("#theme-button").addClass("button-border");
    $("#friend-button").removeClass("button-border");
  });

  $(".high-score").click(function() {
    $(".high-score").addClass("scoreboard-highlight");
    $(".enemies-defeated").removeClass("scoreboard-highlight");
    $(".time-played").removeClass("scoreboard-highlight");
    $(".total-scores").removeClass("scoreboard-highlight");
    $(".last-play").removeClass("scoreboard-highlight");
    $(".high-score-score").show();
    $(".enemies-defeated-score").hide();
    $(".time-played-score").hide();
    $(".total-scores-score").hide();
    $(".last-play-score").hide();
  });

  $(".enemies-defeated").click(function() {
    $(".high-score").removeClass("scoreboard-highlight");
    $(".enemies-defeated").addClass("scoreboard-highlight");
    $(".time-played").removeClass("scoreboard-highlight");
    $(".total-scores").removeClass("scoreboard-highlight");
    $(".last-play").removeClass("scoreboard-highlight");
    $(".high-score-score").hide();
    $(".enemies-defeated-score").show();
    $(".time-played-score").hide();
    $(".total-scores-score").hide();
    $(".last-play-score").hide();
  });

  $(".time-played").click(function() {
    $(".high-score").removeClass("scoreboard-highlight");
    $(".enemies-defeated").removeClass("scoreboard-highlight");
    $(".time-played").addClass("scoreboard-highlight");
    $(".total-scores").removeClass("scoreboard-highlight");
    $(".last-play").removeClass("scoreboard-highlight");
    $(".high-score-score").hide();
    $(".enemies-defeated-score").hide();
    $(".time-played-score").show();
    $(".total-scores-score").hide();
    $(".last-play-score").hide();
  });

  $(".total-scores").click(function() {
    $(".high-score").removeClass("scoreboard-highlight");
    $(".enemies-defeated").removeClass("scoreboard-highlight");
    $(".time-played").removeClass("scoreboard-highlight");
    $(".total-scores").addClass("scoreboard-highlight");
    $(".last-play").removeClass("scoreboard-highlight");
    $(".high-score-score").hide();
    $(".enemies-defeated-score").hide();
    $(".time-played-score").hide();
    $(".total-scores-score").show();
    $(".last-play-score").hide();
  });

  $(".last-play").click(function() {
    $(".high-score").removeClass("scoreboard-highlight");
    $(".enemies-defeated").removeClass("scoreboard-highlight");
    $(".time-played").removeClass("scoreboard-highlight");
    $(".total-scores").removeClass("scoreboard-highlight");
    $(".last-play").addClass("scoreboard-highlight");
    $(".high-score-score").hide();
    $(".enemies-defeated-score").hide();
    $(".time-played-score").hide();
    $(".total-scores-score").hide();
    $(".last-play-score").show();
  });

  $(".sprite-one").click(function() {
    $(".sprite-one").addClass("profile-quadrant-click")
    $(".sprite-two").removeClass("profile-quadrant-click")
    $(".sprite-three").removeClass("profile-quadrant-click")
    $(".sprite-four").removeClass("profile-quadrant-click")
  });

  $(".sprite-two").click(function() {
    $(".sprite-one").removeClass("profile-quadrant-click")
    $(".sprite-two").addClass("profile-quadrant-click")
    $(".sprite-three").removeClass("profile-quadrant-click")
    $(".sprite-four").removeClass("profile-quadrant-click")
  });

  $(".sprite-three").click(function() {
    $(".sprite-one").removeClass("profile-quadrant-click")
    $(".sprite-two").removeClass("profile-quadrant-click")
    $(".sprite-three").addClass("profile-quadrant-click")
    $(".sprite-four").removeClass("profile-quadrant-click")
  });

  $(".sprite-four").click(function() {
    $(".sprite-one").removeClass("profile-quadrant-click")
    $(".sprite-two").removeClass("profile-quadrant-click")
    $(".sprite-three").removeClass("profile-quadrant-click")
    $(".sprite-four").addClass("profile-quadrant-click")
  });

  $(".theme-one").click(function() {
    $(".theme-one").addClass("profile-quadrant-click")
    $(".theme-two").removeClass("profile-quadrant-click")
    $(".theme-three").removeClass("profile-quadrant-click")
    $(".theme-four").removeClass("profile-quadrant-click")
  });

  $(".theme-two").click(function() {
    $(".theme-one").removeClass("profile-quadrant-click")
    $(".theme-two").addClass("profile-quadrant-click")
    $(".theme-three").removeClass("profile-quadrant-click")
    $(".theme-four").removeClass("profile-quadrant-click")
  });

  $(".theme-three").click(function() {
    $(".theme-one").removeClass("profile-quadrant-click")
    $(".theme-two").removeClass("profile-quadrant-click")
    $(".theme-three").addClass("profile-quadrant-click")
    $(".theme-four").removeClass("profile-quadrant-click")
  });

  $(".theme-four").click(function() {
    $(".theme-one").removeClass("profile-quadrant-click")
    $(".theme-two").removeClass("profile-quadrant-click")
    $(".theme-three").removeClass("profile-quadrant-click")
    $(".theme-four").addClass("profile-quadrant-click")
  });

  //THIS IS FOR C# SEARCH FUNCTION DO NOT TOUCH
  $("#search-button").submit(function(event){
    event.preventDefault();
    console.log("stuff happened");
    
  });
});

$(document).ready(function() {
  $(".menu-circle").click(function() {
    $(".menu-circle").toggleClass("x-active");
    $(".menu-game").toggle();
    $(".menu-profile").toggle();
    $(".menu-main").toggle();
    $(".menu-leaderboard").toggle();
    $(".menu-search").toggle();
    // $(".search-form").fadeToggle();
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
  });

  $("#theme-button").click(function() {
    $(".profile-themes-hide").show();
    $(".themes-prompt").show();
    $(".friends-prompt").hide();
    $(".sprites-prompt").hide();
    $(".profile-sprites-hide").hide();
    $(".profile-friends-hide").hide();
  });

  $("#friend-button").click(function() {
    $(".profile-friends-hide").show();
    $(".friends-prompt").show();
    $(".themes-prompt").hide();
    $(".sprites-prompt").hide();
    $(".profile-sprites-hide").hide();
    $(".profile-themes-hide").hide();
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
});

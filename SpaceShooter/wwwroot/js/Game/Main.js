Game.initObjects = function() {
  Game.player = new Player();
  Game.player.position = Game.player.spawnPosition;
  Game.spawnObject(Game.player);

  Game.spawnObject(new EnemySpawner());

  Game.addScreenBounds();
}

Game.gameOver = function() {

  Game.isRunning = false;
  Game.showGameOverScreen();
  Game.submitStats();
}
Game.showGameOverScreen = function() {
  Game.spawnText(new TextObject(
    "GAME OVER",
    new Vector(Game.viewport.width / 2, 50),
    {
      font: "72px sans-serif",
      fillStyle: "rgb(255,255,255)",
      alignment: "center",
    }));

  Game.spawnText(new TextObject(
    "Score: " + Game.player.score,
    new Vector(Game.viewport.width / 2, 150),
    {
      font: "35px sans-serif",
      fillStyle: "rgb(255,255,255)",
      alignment: "center",
    }));
  Game.spawnText(new TextObject(
    "Enemies Destroyed: " + Game.player.numEnemiesDestroyed,
    new Vector(Game.viewport.width / 2, 200),
    {
      font: "35px sans-serif",
      fillStyle: "rgb(255,255,255)",
      alignment: "center",
    }));
    // TODO: Do time conversion
  var playTime = Math.floor((new Date().getTime() - Game.startTime) / 1000);
  Game.spawnText(new TextObject(
    "Play Time: " + playTime + "s",
    new Vector(Game.viewport.width / 2, 250),
    {
      font: "35px sans-serif",
      fillStyle: "rgb(255,255,255)",
      alignment: "center",
    }));
  Game.spawnText(new TextObject(
    "Press 'r' to restart",
    new Vector(Game.viewport.width / 2, 300),
    {
      font: "35px sans-serif",
      fillStyle: "rgb(255,255,255)",
      alignment: "center",
    }));
}
Game.submitStats = function() {
  // TODO: error handling
  var stats = {
    score: Game.player.score,
    enemiesDestroyed: Game.player.numEnemiesDestroyed,
    playTime: new Date().getTime() - Game.startTime
  };
  $.post("/gamepage/submit-stats", stats);
}

// TODO: Be less hacky
Game.addScreenBounds = function() {
  var screenBoundsT = new GameObject(null);
  screenBoundsT.type = "bounds";
  screenBoundsT.position = new Vector(0, 0);
  screenBoundsT.collisionBox = new Rect(0, -100, Game.viewport.width, 100);
  Game.spawnObject(screenBoundsT);

  var screenBoundsB = new GameObject(null);
  screenBoundsB.type = "bounds";
  screenBoundsB.position = new Vector(0, 0);
  screenBoundsB.collisionBox = new Rect(0, Game.viewport.height, Game.viewport.width, 100);
  Game.spawnObject(screenBoundsB);

  var screenBoundsL = new GameObject(null);
  screenBoundsL.type = "bounds";
  screenBoundsL.position = new Vector(0, 0);
  screenBoundsL.collisionBox = new Rect(-100, 0, 100, Game.viewport.height);
  Game.spawnObject(screenBoundsL);

  var screenBoundsR = new GameObject(null);
  screenBoundsR.type = "bounds";
  screenBoundsR.position = new Vector(0, 0);
  screenBoundsR.collisionBox = new Rect(Game.viewport.width, 0, 100, Game.viewport.height);
  Game.spawnObject(screenBoundsR);
}

$(document).ready(function() {
  var canvas = document.getElementById("game-window");
  Game.start(canvas);
  Game.initObjects();
  Game.drawDebugInfo = true;

  document.onkeydown = function(event) {
    var key = event.key;
    if (key === "ArrowRight") {
      Game.player.keyDown.right = true;
      event.preventDefault();
    }
    else if (key === "ArrowLeft") {
      Game.player.keyDown.left = true;
      event.preventDefault();
    }
    else if (key === "ArrowUp") {
      Game.player.keyDown.up = true;
      event.preventDefault();
    }
    else if (key === "ArrowDown") {
      Game.player.keyDown.down = true;
      event.preventDefault();
    }
    else if (key === " ") {
      Game.player.keyDown.fire = true;
      event.preventDefault();
    }
    else if (key === "F2") {
      Game.player.keyDown.drawDebug = true;
      event.preventDefault();
    }
  };
  document.onkeyup = function(event) {
    var key = event.key;
    if (key === "ArrowRight") {
      Game.player.keyDown.right = false;
    }
    else if (key === "ArrowLeft") {
      Game.player.keyDown.left = false;
    }
    else if (key === "ArrowUp") {
      Game.player.keyDown.up = false;
    }
    else if (key === "ArrowDown") {
      Game.player.keyDown.down = false;
    }
    else if (key === " ") {
      Game.player.keyDown.fire = false;
    }
    else if (key === "F2") {
      Game.player.keyDown.drawDebug = false;
      event.preventDefault();
    }
  };
});

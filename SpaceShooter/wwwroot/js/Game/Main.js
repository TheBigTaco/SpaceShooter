Game.keyDown = {
  left: false,
  right: false,
  up: false,
  down: false,
  fire: false,
  restart: false,
  drawDebug: false,
}
Game.initObjects = function() {
  Game.currentScene = new StartScreenScene();

  Game.player = new Player();
  Game.player.position = Game.player.spawnPosition;
  Game.spawnObject(Game.player);

  Game.spawnObject(new EnemySpawner());

  Game.addScreenBounds();
}
Game.gameOver = function() {
  Game.isRunning = false;
  Game.submitStats();
  Game.currentScene = new GameOverScene();
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
  Game.initialize(canvas);
  Game.initObjects();

  document.onkeydown = function(event) {
    var key = event.key;
    if (key === "ArrowRight") {
      Game.keyDown.right = true;
      event.preventDefault();
    }
    else if (key === "ArrowLeft") {
      Game.keyDown.left = true;
      event.preventDefault();
    }
    else if (key === "ArrowUp") {
      Game.keyDown.up = true;
      event.preventDefault();
    }
    else if (key === "ArrowDown") {
      Game.keyDown.down = true;
      event.preventDefault();
    }
    else if (key === " ") {
      Game.keyDown.fire = true;
      event.preventDefault();
    }
    else if (key === "r") {
      Game.keyDown.restart = true;
      event.preventDefault();
    }
    else if (key === "F2") {
      Game.keyDown.drawDebug = true;
      event.preventDefault();
    }
  };
  document.onkeyup = function(event) {
    var key = event.key;
    if (key === "ArrowRight") {
      Game.keyDown.right = false;
    }
    else if (key === "ArrowLeft") {
      Game.keyDown.left = false;
    }
    else if (key === "ArrowUp") {
      Game.keyDown.up = false;
    }
    else if (key === "ArrowDown") {
      Game.keyDown.down = false;
    }
    else if (key === " ") {
      Game.keyDown.fire = false;
    }
    else if (key === "r") {
      Game.keyDown.restart = false;
    }
    else if (key === "F2") {
      Game.keyDown.drawDebug = false;
    }
  };
});

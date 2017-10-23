function Game() {
  this.gameObjects = [];
  this.tickNumber = 0;
  this.isRunning = false;
  this.viewport = new Viewport(this.gameObjects);
  this.player = new Player();
}

// TODO: fix hacked-in player
Game.prototype.start = function() {
  this.isRunning = true;
  this.gameObjects.push(this.player);
  this.main();
}

Game.prototype.main = function() {
  var currentTickTime = new Date().getTime();
  var prevTickTime = currentTickTime;
  var currentTime = new Date().getTime();
  var prevTime = currentTime;
  var dT = 0;
  setInterval(function() {
    currentTime = new Date().getTime();
    dT += currentTime - prevTime;
    if (dT >= 10) {
      prevTickTime = currentTickTime;
      currentTickTime = new Date().getTime();
      this.tickNumber++;
      dT %= 10;
      this.updateGameObjects();
    }
    prevTime = currentTime;
  }.bind(this), 1);
}

Game.prototype.updateGameObjects = function() {
  if (this.gameObjects.length > 0) {
    this.gameObjects.forEach(function(gameObject) {
      gameObject.update();
    });
  }
}

function Viewport(gameObjects) {
  this.gameObjects = gameObjects;
  this.draw();
}

Viewport.prototype.draw = function() {
  var canvas = document.getElementById("canvas");
  if (canvas.getContext) {
    var ctx = canvas.getContext('2d');
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    ctx.save();

    // do the rectangle
    if(this.gameObjects.length > 0) {
      ctx.fillStyle = 'rgb(200, 0, 0)';
      ctx.fillRect(this.gameObjects[0].position.x, this.gameObjects[0].position.y, 100, 100);
    }
    ctx.restore();
    window.requestAnimationFrame(this.draw.bind(this));
  }
}

function Player() {
  this.keyDown = {
    left: false,
    right: false,
    up: false,
    down: false
  };
  this.position = new Vector(50, 50);
  this.velocity = new Vector(0, 0);
}

Player.prototype.update = function() {
  this.getKeyInput();
  this.position = Vector.Add(this.position, this.velocity);
}

Player.prototype.getKeyInput = function() {
  if (this.keyDown.left === true) {
    this.velocity.x = -3;
  }
  else if (this.keyDown.right === true) {
    this.velocity.x = 3;
  }
  else {
    this.velocity.x = 0;
  }
  if (this.keyDown.up === true) {
    this.velocity.y = -3;
  }
  else if (this.keyDown.down === true) {
    this.velocity.y = 3;
  }
  else {
    this.velocity.y = 0;
  }
}

function Vector(x, y) {
  this.x = x;
  this.y = y;
}

Vector.Add = function(Vector1, Vector2) {
  var result = new Vector(0, 0);
  result.x = Vector1.x + Vector2.x;
  result.y = Vector1.y + Vector2.y;
  return result;
}

$(document).ready(function() {
  var game = new Game();
  game.start();

  document.onkeydown = function(event) {
    var key = event.key;
    if (key === "ArrowRight") {
      game.player.keyDown.right = true;
    }
    else if (key === "ArrowLeft") {
      game.player.keyDown.left = true;
    }
    else if (key === "ArrowUp") {
      game.player.keyDown.up = true;
    }
    else if (key === "ArrowDown") {
      game.player.keyDown.down = true;
    }
  };
  document.onkeyup = function(event) {
    var key = event.key;
    if (key === "ArrowRight") {
      game.player.keyDown.right = false;
    }
    else if (key === "ArrowLeft") {
      game.player.keyDown.left = false;
    }
    else if (key === "ArrowUp") {
      game.player.keyDown.up = false;
    }
    else if (key === "ArrowDown") {
      game.player.keyDown.down = false;
    }
  };
});

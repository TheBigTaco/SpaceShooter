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
  var dT = 0;
  setInterval(function() {
    currentTickTime = new Date().getTime();
    this.updateGameObjects(currentTickTime - prevTickTime);
    prevTickTime = currentTickTime;
  }.bind(this), 10);
}

Game.prototype.updateGameObjects = function(dT) {
  if (this.gameObjects.length > 0) {
    this.gameObjects.forEach(function(gameObject) {
      gameObject.update(dT);
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
  this.maxVelocity = 350;
}

Player.prototype.update = function(dT) {
  var moveVector = this.getKeyInput();
  this.velocity = Vector.scale(this.maxVelocity * dT / 1000, moveVector);
  this.position = Vector.add(this.position, this.velocity);
}

Player.prototype.getKeyInput = function() {
  var output = new Vector(0, 0);
  if (this.keyDown.left === true) {
    output.x = -1;
  }
  else if (this.keyDown.right === true) {
    output.x = 1;
  }
  else {
    output.x = 0;
  }
  if (this.keyDown.up === true) {
    output.y = -1;
  }
  else if (this.keyDown.down === true) {
    output.y = 1;
  }
  else {
    output.y = 0;
  }
  return output;
}

function Vector(x, y) {
  this.x = x;
  this.y = y;
}

Vector.add = function(vector1, vector2) {
  var result = new Vector(0, 0);
  result.x = vector1.x + vector2.x;
  result.y = vector1.y + vector2.y;
  return result;
}

Vector.scale = function(scaleFactor, vector)
{
  return new Vector(scaleFactor * vector.x, scaleFactor * vector.y);
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

class Game {
  constructor() {
    this.gameObjects = [];
    this.tickNumber = 0;
    this.isRunning = false;
    this.viewport = new Viewport(this.gameObjects);
    this.player = null;
  }
  // TODO: fix hacked-in player
  start() {
    this.isRunning = true;
    this.initObjects();
    this.main();
  }
  initObjects() {
    this.player = new Player();
    this.player.position = new Vector(50, 50);
    this.player.collisionBox = new Rect(0, 0, 100, 100);
    this.gameObjects.push(this.player);

    var obj1 = new GameObject();
    obj1.position = new Vector(200, 200);
    obj1.collisionBox = new Rect(0, 0, 100, 100);
    this.gameObjects.push(obj1);
  }
  main() {
    var currentTickTime = new Date().getTime();
    var prevTickTime = currentTickTime;
    var dT = 0;
    setInterval(function() {
      currentTickTime = new Date().getTime();
      this.updateGameObjects(currentTickTime - prevTickTime);
      prevTickTime = currentTickTime;
    }.bind(this), 10);
  }
  updateGameObjects(dT) {
    if (this.gameObjects.length > 0) {
      this.gameObjects.forEach(function(gameObject) {
        gameObject.update(dT);
      });
    }
  }
}

class Viewport {
  constructor(gameObjects) {
    this.gameObjects = gameObjects;
    this.draw();
  }
  draw() {
    var canvas = document.getElementById("canvas");
    if (canvas.getContext) {
      var ctx = canvas.getContext('2d');
      ctx.clearRect(0, 0, canvas.width, canvas.height);
      ctx.save();

      // draw all gameobjects based on collisionBox
      this.gameObjects.forEach(function(gameObject) {
        if(gameObject.type === "player") {
          ctx.fillStyle = 'rgb(200, 0, 0)';
        }
        else {
          ctx.fillStyle = 'rgb(0,200,0)';
        }
        ctx.fillRect(gameObject.position.x, gameObject.position.y, gameObject.collisionBox.width, gameObject.collisionBox.height);

      }.bind(this));
      ctx.restore();
      window.requestAnimationFrame(this.draw.bind(this));
    }
  }
}

class GameObject {
  constructor() {
    this.type = "game-object";
    this.position = null;
    this.velocity = new Vector(0, 0);
    this.collisionBox = null;
  }
  update(dT) {
    this.updatePosition(dT);
  }
  updatePosition(dT) {
    var deltaPostion = Vector.scale(dT, this.velocity);
    this.position = Vector.add(this.position, deltaPostion);
  }
  onCollision(collideTarget) {
    // TODO
  }
}

class Player extends GameObject {
  constructor() {
    super();
    this.type = "player";
    this.keyDown = {
      left: false,
      right: false,
      up: false,
      down: false
    }
    this.maxVelocity = 350;
  }
  update(dT) {
    this.velocity = Vector.scale(this.maxVelocity / 1000,  this.getKeyInput());
    super.update(dT);
  }
  getKeyInput() {
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
}

class Vector {
  constructor(x, y) {
    this.x = x;
    this.y = y;
  }
  static add(vector1, vector2) {
    var result = new Vector(0, 0);
    result.x = vector1.x + vector2.x;
    result.y = vector1.y + vector2.y;
    return result;
  }
  static scale(scaleFactor, vector) {
    return new Vector(scaleFactor * vector.x, scaleFactor * vector.y);
  }
}

class Rect {
  constructor(x, y, width, height) {
    this.x = x;
    this.y = y;
    this.width = width;
    this.height = height;
  }
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

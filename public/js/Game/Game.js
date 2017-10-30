class TestGame extends Game {
  constructor(canvas) {
    super(canvas);
    this.player = null;
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
    this.maxVelocity = 250;
  }
  update(dT) {
    this.velocity = Vector.scale(this.maxVelocity / 1000,  this.getKeyInput());
    super.update(dT);
  }
  onCollision(collisionResult) {
    if (collisionResult.intersection.width < collisionResult.intersection.height) {
      if (this.getCollisionBoxPosition().x + this.collisionBox.width < collisionResult.collideTarget.getCollisionBoxPosition().x + collisionResult.collideTarget.collisionBox.width) {
        this.position.x -= collisionResult.intersection.width;
      }
      else if (this.getCollisionBoxPosition().x > collisionResult.collideTarget.getCollisionBoxPosition().x) {
        this.position.x += collisionResult.intersection.width;
      }
    }
    else {
      if (this.getCollisionBoxPosition().y + this.collisionBox.height < collisionResult.collideTarget.getCollisionBoxPosition().y + collisionResult.collideTarget.collisionBox.height) {
        this.position.y -= collisionResult.intersection.height;
      }
      else if (this.getCollisionBoxPosition().y > collisionResult.collideTarget.getCollisionBoxPosition().y) {
        this.position.y += collisionResult.intersection.height;
      }
    }
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

$(document).ready(function() {
  var canvas = document.getElementById("game-window");
  var game = new TestGame(canvas);
  game.initObjects();
  game.viewport.drawDebugInfo = true;
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

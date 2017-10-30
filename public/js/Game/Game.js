Game.initObjects = function() {
  var playerSprite =  new GameObjectSprite("img/player/player.png", 32, 32);
  Game.player = new Player(playerSprite);
  Game.player.position = new Vector(50, 50);
  Game.player.collisionBox = new Rect(4, 8, 24, 16);
  Game.spawnObject(Game.player);

  var obj1 = new GameObject(null);
  obj1.position = new Vector(200, 200);
  obj1.collisionBox = new Rect(0, 0, 100, 100);
  Game.spawnObject(obj1);
}

class Player extends GameObject {
  constructor(playerSprite) {
    super(playerSprite);
    this.type = "player";
    this.keyDown = {
      left: false,
      right: false,
      up: false,
      down: false,
      fire: false
    }
    this.maxVelocity = 250;
  }
  update(dT) {
    this.velocity = Vector.scale(this.maxVelocity,  this.getMovementInput());
    this.getActionInput();
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
  getMovementInput() {
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
    // normalize velocity for diagonals
    if(output.x && output.y) {
      output.x *= Math.SQRT1_2;
      output.y *= Math.SQRT1_2;
    }
    return output;
  }
  getActionInput() {
    if (this.keyDown.fire === true) {
      this.fire();
      this.keyDown.fire = false;
    }
  }
  fire() {
    console.log("pew");
    Game.spawnObject(new PlayerBullet(this.position));
  }
}

class PlayerBullet extends GameObject {
  constructor(playerPosition) {
    var sprite = new GameObjectSprite("img/player/bullet.png", 5, 2);
    var offset = new Vector(32, 15);
    super(sprite);
    this.type = "player-projectile";
    this.position = Vector.add(playerPosition, offset);
    this.velocity = new Vector(500, 0);
  }
}

$(document).ready(function() {
  var canvas = document.getElementById("game-window");
  Game.initObjects();
  Game.start(canvas);
  Game.viewport.drawDebugInfo = true;

  document.onkeydown = function(event) {
    var key = event.key;
    if (key === "ArrowRight") {
      Game.player.keyDown.right = true;
    }
    else if (key === "ArrowLeft") {
      Game.player.keyDown.left = true;
    }
    else if (key === "ArrowUp") {
      Game.player.keyDown.up = true;
    }
    else if (key === "ArrowDown") {
      Game.player.keyDown.down = true;
    }
    else if (key === " ") {
      Game.player.keyDown.fire = true;
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
  };
});

Game.sprites["player"] = new GameObjectSprite("img/player/player.png", 75, 112, .5);
class Player extends GameObject {
  constructor() {
    super();
    this.type = "player";
    this.sprite = Game.sprites["player"];
    this.collisionBox = new Rect(0, 2, this.sprite.width - 10, this.sprite.height - 4);
    this.score = 0;
    this.numEnemiesDestroyed = 0;
    this.lastFireTime = 0;
    this.fireInterval = 150;
    this.keyDown = {
      left: false,
      right: false,
      up: false,
      down: false,
      fire: false,
      drawDebug: false,
    }
    this.maxVelocity = 350;
  }
  update() {
    this.velocity = Vector.scale(this.maxVelocity,  this.getMovementInput());
    this.getActionInput();
    this.getDebugInput();
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
    }
  }
  getDebugInput() {
    if (this.keyDown.drawDebug === true) {
      Game.drawDebugInfo = !Game.drawDebugInfo;
      this.keyDown["drawDebug"] = false;
    }
  }
  fire() {
    var currentTime = new Date().getTime();
    if (currentTime - this.lastFireTime > this.fireInterval) {
      var offset = new Vector(this.sprite.width, this.sprite.height / 2);
      var spawnPosition = Vector.add(this.position, offset);
      Game.spawnObject(new PlayerBullet(spawnPosition));
      this.lastFireTime = currentTime;
    }
  }
}

Game.sprites["player-bullet-1"] = new GameObjectSprite("img/player/bullets/bullet-1.png", 37, 9, .5);
class PlayerBullet extends GameObject {
  constructor(position) {
    super();
    this.type = "player-projectile";
    this.sprite = Game.sprites["player-bullet-1"];
    this.collisionBox = new Rect(0, 0, this.sprite.width, this.sprite.height);
    var offset = new Vector(0, -this.sprite.height/2);
    this.position = Vector.add(position, offset);
    this.velocity = new Vector(1000, 0);
  }
  update() {
    if (this.position.x > Game.viewport.width) {
      this.despawn();
    }
  }
  onCollision(collisionResult) {
    if (collisionResult.collideTarget.type === "enemy") {
      this.destroy();
    }
  }
}

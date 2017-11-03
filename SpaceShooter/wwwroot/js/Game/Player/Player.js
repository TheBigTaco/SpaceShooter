Game.sprites["player"] =  new GameObjectSprite("img/player/player.png", 32, 32);
class Player extends GameObject {
  constructor() {
    super();
    this.type = "player";
    this.sprite = Game.sprites["player"];
    this.collisionBox = new Rect(4, 8, 24, 16);
    this.keyDown = {
      left: false,
      right: false,
      up: false,
      down: false,
      fire: false,
      drawDebug: false,
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
    if (this.keyDown.drawDebug === true) {
      Game.drawDebugInfo = !Game.drawDebugInfo;
      this.keyDown.drawDebug = false;
    }
  }
  fire() {
    Game.spawnObject(new PlayerBullet(this.position));
  }
}

Game.sprites["player-bullet"] = new GameObjectSprite("img/player/bullet.png", 5, 2);
class PlayerBullet extends GameObject {
  constructor(playerPosition) {
    var offset = new Vector(32, 15);
    super();
    this.type = "player-projectile";
    this.sprite = Game.sprites["player-bullet"];
    this.collisionBox = new Rect(0, 0, 5, 2);
    this.position = Vector.add(playerPosition, offset);
    this.velocity = new Vector(500, 0);
  }
  update(dT) {
    if (this.position.x > Game.viewport.width) {
      this.destroy();
    }
    super.update(dT);
  }
  onCollision(collisionResult) {
    if (collisionResult.collideTarget.type === "enemy") {
      this.destroy();
    }
  }
}

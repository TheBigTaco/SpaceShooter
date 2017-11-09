Game.sprites["player"] = new GameObjectSprite("img/player/player.png", 75, 112, .5);
class Player extends GameObject {
  constructor() {
    super();
    this.type = "player";
    this.sprite = Game.sprites["player"];
    this.spawnPosition = new Vector(50, 200);
    this.respawnInvincibilityTime = 1000;
    this.collisionBox = new Rect(0, 2, this.sprite.width - 10, this.sprite.height - 4);
    this.collidesWith = ["bounds"];
    this.score = 0;
    this.difficulty = 0;
    this.maxDifficulty = 20;
    this.lastDifficultyIncreaseTime = new Date().getTime();
    this.difficultyScaleTime = 1500;
    this.numEnemiesDestroyed = 0;
    this.lives = 3;
    this.isInvincible = false;
    this.lastFireTime = 0;
    this.fireInterval = 150;
    this.maxVelocity = 350;
  }
  update() {
    this.velocity = Vector.scale(this.maxVelocity,  this.getMovementInput());
    this.getActionInput();
    this.getDebugInput();
    var currentTime = new Date().getTime();
    if (currentTime - this.lastDifficultyIncreaseTime > this.difficultyScaleTime) {
      if (this.difficulty < this.maxDifficulty) {
        this.difficulty++;
        this.lastDifficultyIncreaseTime = currentTime;
      }
    }
  }
  onCollision(collisionResult) {
    if (!this.isInvincible && collisionResult.collideTarget.type === "enemy") {
      this.takeDamage();
    }
    super.onCollision(collisionResult);
  }
  takeDamage() {
    this.lives--;
    if (this.lives >= 0) {
      this.deathRespawn();
    }
    else {
      this.destroy();
      Game.gameOver();
    }
  }
  deathRespawn() {
    Animation.flash(this, 10, this.respawnInvincibilityTime);
    this.isInvincible = true;
    setTimeout(function() {
      this.isInvincible = false;
    }.bind(this), this.respawnInvincibilityTime);
  }
  getMovementInput() {
    var output = new Vector(0, 0);
    if (Game.keyDown.left === true) {
      output.x = -1;
    }
    else if (Game.keyDown.right === true) {
      output.x = 1;
    }
    else {
      output.x = 0;
    }
    if (Game.keyDown.up === true) {
      output.y = -1;
    }
    else if (Game.keyDown.down === true) {
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
    if (Game.keyDown.fire === true) {
      this.fire();
    }
  }
  getDebugInput() {
    if (Game.keyDown.drawDebug === true) {
      Game.drawDebugInfo = !Game.drawDebugInfo;
      Game.keyDown["drawDebug"] = false;
    }
  }
  // dont base position on player sprite
  fire() {
    var currentTime = new Date().getTime();
    if (currentTime - this.lastFireTime > this.fireInterval) {
      var offset = new Vector(this.collisionBox.width, this.collisionBox.height / 2);
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
    var offset = new Vector(0, -this.sprite.height/2 + 1);
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
    super.onCollision(collisionResult);
  }
}

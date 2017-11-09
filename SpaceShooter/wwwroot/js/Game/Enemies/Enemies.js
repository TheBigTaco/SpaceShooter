class Enemy extends GameObject {
  constructor() {
    super();
    this.type = "enemy";
    this.scoreValue = 0;
  }
  onCollision(collisionResult) {
    if (collisionResult.collideTarget.type === "player-projectile") {
      Game.player.score += this.scoreValue;
      Game.player.numEnemiesDestroyed += 1;
      this.destroy();
    }
    super.onCollision(collisionResult);
  }
}

Game.sprites["enemy-basic-enemy"] = new GameObjectSprite("img/Enemies/enemy-basic.png", 84, 104, .4);
class BasicEnemy extends Enemy {
  constructor() {
    super();
    this.sprite = Game.sprites["enemy-basic-enemy"];
    this.collisionBox = new Rect(7, 0, this.sprite.width - 13, this.sprite.height);
    this.minSpeed = 400;
    this.maxSpeed = 600;
    this.velocity = new Vector(0, 0);
    this.scoreValue = 100;
  }
  update() {
    var speed = this.minSpeed + (this.maxSpeed - this.minSpeed) * (Game.player.difficulty / Game.player.maxDifficulty);
    this.velocity = new Vector(-speed, 0);
    if (this.position.x < -this.sprite.width) {
      this.despawn();
    }
  }
}

class Enemy extends GameObject {
  constructor() {
    super();
    this.type = "enemy";
  }
  onCollision(collisionResult) {
    if (collisionResult.collideTarget.type === "player-projectile") {
      this.destroy();
    }
  }
}

Game.sprites["enemy-basic-enemy"] = new GameObjectSprite("img/player/player.png", 32, 32);
class BasicEnemy extends Enemy {
  constructor() {
    super();
    this.sprite = Game.sprites["enemy-basic-enemy"];
    this.collisionBox = new Rect(4, 8, 24, 16);
    this.velocity = new Vector(-200, 0);
  }
  update() {
    if (this.position.x < -this.sprite.width) {
      this.despawn();
    }
  }
}

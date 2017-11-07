class EnemySpawner extends GameObject {
  constructor() {
    super();
    this.type = "spawner";
    this.spawnRate = 500;
    this.lastSpawnTime = 0;
  }
  update() {
    var currentTime = new Date().getTime();
    if (currentTime - this.lastSpawnTime > this.spawnRate) {
      this.spawnBasicEnemy();
      this.lastSpawnTime = currentTime;
    }
  }
  spawnBasicEnemy() {
    var enemy = new BasicEnemy();
    var r = Game.viewport.height * Math.random();
    enemy.position = new Vector(Game.viewport.width, r);
    Game.spawnObject(enemy);
  }
}

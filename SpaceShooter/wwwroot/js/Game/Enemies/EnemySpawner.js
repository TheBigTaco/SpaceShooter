class EnemySpawner extends GameObject {
  constructor() {
    super();
    this.type = "spawner";
    this.minSpawnDelay = 50;
    this.maxSpawnDelay = 500;
    this.lastSpawnTime = 0;
  }

  update() {
    var spawnDelay = this.maxSpawnDelay - (this.maxSpawnDelay - this.minSpawnDelay) * (Game.player.difficulty / Game.player.maxDifficulty);
    var currentTime = new Date().getTime();
    if (currentTime - this.lastSpawnTime > spawnDelay) {
      console.log(spawnDelay);
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

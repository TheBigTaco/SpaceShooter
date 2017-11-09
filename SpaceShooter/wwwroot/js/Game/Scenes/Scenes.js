class StartScreenScene extends Scene {
  constructor() {
    super();
    this.name = "start-screen";
    this.gameStartText = new TextObject(
      "Press space to start",
      new Vector(Game.viewport.width / 2, 100),
      {
        font: "45px Orbitron",
        fillStyle: "rgb(255,255,255)",
        alignment: "center",
      });
    Game.spawnText(this.gameStartText);
  }
  update() {
    if (Game.keyDown["fire"]) {
      Game.start();
      Game.textObjects = {};
      Game.currentScene = new GamePlayingScene();
    }
  }
}
class GamePlayingScene extends Scene {
  constructor() {
    super();
    this.name = "game-playing";
    this.hudLives = new TextObject(
      "Lives: " + Game.player.lives,
      new Vector(20, 20),
      {
        font: "20px Orbitron",
        fillStyle: "rgb(255,255,255)",
        alignment: "left",
      });
    Game.spawnText(this.hudLives);
    this.hudScore = new TextObject(
      "Score: " + Game.player.score,
      new Vector(Game.viewport.width - 20, 20),
      {
        font: "20px Orbitron",
        fillStyle: "rgb(255,255,255)",
        alignment: "right",
      });
    Game.spawnText(this.hudScore);
  }
  update() {
    this.hudLives.text = "Lives: " + Game.player.lives;
    this.hudScore.text = "Score: " + Game.player.score;
  }
}
class GameOverScene extends Scene {
  constructor() {
    super();
    this.name = "game-over";
    var textYOffset = 50;

    Game.spawnText(new TextObject(
      "GAME OVER",
      new Vector(Game.viewport.width / 2, 50 + textYOffset),
      {
        font: "72px Orbitron",
        fillStyle: "rgb(255,255,255)",
        alignment: "center",
      }));

    Game.spawnText(new TextObject(
      "Score: " + Game.player.score,
      new Vector(Game.viewport.width / 2, 150 + textYOffset),
      {
        font: "35px Orbitron",
        fillStyle: "rgb(255,255,255)",
        alignment: "center",
      }));

    Game.spawnText(new TextObject(
      "Enemies Destroyed: " + Game.player.numEnemiesDestroyed,
      new Vector(Game.viewport.width / 2, 200 + textYOffset),
      {
        font: "35px Orbitron",
        fillStyle: "rgb(255,255,255)",
        alignment: "center",
      }));

    // TODO: Do time conversion
    var playTime = Math.floor((new Date().getTime() - Game.startTime) / 1000);
    Game.spawnText(new TextObject(
      "Play Time: " + playTime + "s",
      new Vector(Game.viewport.width / 2, 250 + textYOffset),
      {
        font: "35px Orbitron",
        fillStyle: "rgb(255,255,255)",
        alignment: "center",
      }));
    Game.spawnText(new TextObject(
      "Press 'R' to restart",
      new Vector(Game.viewport.width / 2, 300 + textYOffset),
      {
        font: "35px Orbitron",
        fillStyle: "rgb(255,255,255)",
        alignment: "center",
      }));
  }
  update() {
    if (Game.keyDown["restart"]) {
      Game.keyDown["restart"] = false;
      // TODO: not this
      location.reload();
    }
  }
}

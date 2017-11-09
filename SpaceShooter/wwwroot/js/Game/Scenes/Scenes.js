class StartScreenScene extends Scene {
  constructor() {
    super();
    this.name = "start-screen";
  }
  update() {

  }
}
class GamePlayingScene extends Scene {
  constructor() {
    super();
    this.name = "game-playing";
  }
  update() {

  }
}
class GameOverScene extends Scene {
  constructor() {
    super();
    this.name = "game-over";
  }
  update() {
    if (Game.keyDown["restart"]) {
      Game.keyDown["restart"] = false;
      // TODO: not this
      location.reload();
    }
  }
}

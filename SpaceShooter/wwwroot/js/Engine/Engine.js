// Game is a static class
var Game = {
  gameObjects: {},
  lastObjectId: 0,
  sprites: {},
  objectsToBeDisposed: [],
  isRunning: false,
  drawDebugInfo: false,
  tickNumber: 0,
  viewport: null,
};
Game.start = function(canvas) {
  Game.isRunning = true;
  Game.viewport = new Viewport(canvas);
  Game.main();
}
Game.main = function() {
  var currentTickTime = new Date().getTime();
  var prevTickTime = currentTickTime;
  setInterval(function() {
    currentTickTime = new Date().getTime();
    Game.updateGameObjects(currentTickTime - prevTickTime);
    Game.checkCollisions();
    Game.deleteDisposedObjects();
    prevTickTime = currentTickTime;
  }, 10);
}
Game.updateGameObjects = function(dT) {
  if (Object.keys(Game.gameObjects).length > 0) {
    Object.keys(Game.gameObjects).forEach(function(key) {
      Game.gameObjects[key].update(dT);
    });
  }
}
// TODO: be more efficient
Game.checkCollisions = function() {
  for (var i = 0; i < Object.keys(Game.gameObjects).length; i++) {
    var keyI = Object.keys(Game.gameObjects)[i];
    for (var j = i + 1; j < Object.keys(Game.gameObjects).length; j++) {
      var keyJ = Object.keys(Game.gameObjects)[j]
      if (Game.gameObjects[keyI].collisionBox !== null && Game.gameObjects[keyJ].collisionBox !== null) {
        var collisionOverlap = Game.gameObjects[keyI].getCollisionBoxPosition().calcIntersectionWith(Game.gameObjects[keyJ].getCollisionBoxPosition());
        if(collisionOverlap != null) {
          Game.gameObjects[keyI].onCollision(new CollisionResult(Game.gameObjects[keyJ], collisionOverlap));
          Game.gameObjects[keyJ].onCollision(new CollisionResult(Game.gameObjects[keyI], collisionOverlap));
        }
      }
    }
  }
}
Game.spawnObject = function(gameObject) {
  Game.lastObjectId++;
  gameObject.id = Game.lastObjectId;
  Game.gameObjects[gameObject.id] = gameObject;
}
Game.markAsDisposed = function(gameObject) {
  gameObject.isDisposed = true;
  Game.objectsToBeDisposed.push(gameObject);
}
Game.deleteDisposedObjects = function() {
  Game.objectsToBeDisposed.forEach(function(gameObject) {
    delete Game.gameObjects[gameObject.id];
  });
Game.objectsToBeDisposed = [];
}

class Viewport {
  constructor(canvas) {
    this.canvas = canvas;
    this.width = this.canvas.width;
    this.height = this.canvas.height;
    this.draw();
  }
  draw() {
    if (this.canvas.getContext) {
      var ctx = this.canvas.getContext('2d');
      ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);
      ctx.save();

      Object.keys(Game.gameObjects).forEach(function(key) {
        // draw sprite
        if (Game.gameObjects[key].sprite) {
          ctx.drawImage(Game.gameObjects[key].sprite.img, Game.gameObjects[key].position.x, Game.gameObjects[key].position.y, Game.gameObjects[key].sprite.width, Game.gameObjects[key].sprite.height);
        }
        // draw collision boxes
        if (Game.drawDebugInfo && Game.gameObjects[key].collisionBox) {
          ctx.fillStyle = 'rgba(0,200,0,0.3)';
          ctx.fillRect(Game.gameObjects[key].getCollisionBoxPosition().x, Game.gameObjects[key].getCollisionBoxPosition().y, Game.gameObjects[key].collisionBox.width, Game.gameObjects[key].collisionBox.height);
        }
      }.bind(this));
      ctx.restore();
      window.requestAnimationFrame(this.draw.bind(this));
    }
  }
}

class GameObject {
  constructor() {
    this.id = null;
    this.type = "game-object";
    this.isDisposed = false;
    this.sprite = null;
    this.collisionBox = null;
    this.position = null;
    this.velocity = new Vector(0, 0);
  }
  update(dT) {
    this.updatePosition(dT);
  }
  updatePosition(dT) {
    if (this.position != null) {
      var deltaPostion = Vector.scale(dT / 1000, this.velocity);
      this.position = Vector.add(this.position, deltaPostion);
    }
  }
  destroy() {
    Game.markAsDisposed(this);
  }
  getCollisionBoxPosition() {
    return (this.collisionBox != null) ? this.collisionBox.addOffsetVector(this.position) : null;
  }
  onCollision(collisionResult) {

  }
}

class GameObjectSprite {
  constructor(imgSrc, width, height) {
    this.imgSrc = imgSrc;
    this.width = width;
    this.height = height;
    this.img = new Image();
    this.img.src = imgSrc;
  }
}

class Vector {
  constructor(x, y) {
    this.x = x;
    this.y = y;
  }
  static add(vector1, vector2) {
    var result = new Vector(0, 0);
    result.x = vector1.x + vector2.x;
    result.y = vector1.y + vector2.y;
    return result;
  }
  static scale(scaleFactor, vector) {
    return new Vector(scaleFactor * vector.x, scaleFactor * vector.y);
  }
  isInRectBounds(rect) {
    return (
      this.x > rect.x && this.x < rect.x + rect.width &&
      this.y > rect.y && this.y < rect.y + rect.height
    );
  }
}

class Rect {
  constructor(x, y, width, height) {
    this.x = x;
    this.y = y;
    this.width = width;
    this.height = height;
  }
  intersectsWith(otherRect) {
    return (
      this.x < otherRect.x + otherRect.width &&
      this.x + this.width > otherRect.x &&
      this.y < otherRect.y + otherRect.height &&
      this.y + this.height > otherRect.y
    );
  }
  calcIntersectionWith(otherRect) {
    if (this.intersectsWith(otherRect)) {
      var x1 = (this.x > otherRect.x) ? this.x : otherRect.x;
      var x2 = (this.x + this.width < otherRect.x + otherRect.width) ? this.x + this.width : otherRect.x + otherRect.width;
      var y1 = (this.y > otherRect.y) ? this.y : otherRect.y;
      var y2 = (this.y + this.height < otherRect.y + otherRect.height) ? this.y + this.height : otherRect.y + otherRect.height;
      return new Rect(x1, y1, x2 - x1, y2 - y1);
    }
    else {
      return null;
    }
    var x = (this.x + this.width) - other.x
  }
  addOffsetVector(offsetVector) {
    return new Rect(this.x + offsetVector.x, this.y + offsetVector.y, this.width, this.height);
  }
}

class CollisionResult {
  constructor(collideTarget, intersection) {
    this.collideTarget = collideTarget;
    this.intersection = intersection;
  }
}

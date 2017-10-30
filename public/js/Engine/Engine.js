// Game is a static class
var Game = {
  gameObjects: [],
  isRunning: false,
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
    prevTickTime = currentTickTime;
  }, 10);
}
Game.updateGameObjects = function(dT) {
  if (Game.gameObjects.length > 0) {
    Game.gameObjects.forEach(function(gameObject) {
      gameObject.update(dT);
    });
  }
}
// TODO: be more efficient
Game.checkCollisions = function() {
  for (var i = 0; i < Game.gameObjects.length; i++) {
    for (var j = i + 1; j < Game.gameObjects.length; j++) {
      if (Game.gameObjects[i].collisionBox !== null && Game.gameObjects[j].collisionBox !== null) {
        var collisionOverlap = Game.gameObjects[i].getCollisionBoxPosition().calcIntersectionWith(Game.gameObjects[j].getCollisionBoxPosition());
        if(collisionOverlap != null) {
          Game.gameObjects[i].onCollision(new CollisionResult(Game.gameObjects[j], collisionOverlap));
          Game.gameObjects[j].onCollision(new CollisionResult(Game.gameObjects[i], collisionOverlap));
        }
      }
    }
  }
}
Game.spawnObject = function(gameObject) {
  Game.gameObjects.push(gameObject);
}

class Viewport {
  constructor(canvas) {
    this.canvas = canvas;
    this.drawDebugInfo = false;
    this.draw();
  }
  draw() {
    if (this.canvas.getContext) {
      var ctx = this.canvas.getContext('2d');
      ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);
      ctx.save();

      Game.gameObjects.forEach(function(gameObject) {
        // draw sprite
        if (gameObject.sprite) {
          ctx.drawImage(gameObject.sprite.img, gameObject.position.x, gameObject.position.y, gameObject.sprite.width, gameObject.sprite.height);
        }
        // draw collision boxes
        if (this.drawDebugInfo && gameObject.collisionBox) {
          ctx.fillStyle = 'rgba(0,200,0,0.3)';
          ctx.fillRect(gameObject.getCollisionBoxPosition().x, gameObject.getCollisionBoxPosition().y, gameObject.collisionBox.width, gameObject.collisionBox.height);
        }
      }.bind(this));
      ctx.restore();
      window.requestAnimationFrame(this.draw.bind(this));
    }
  }
}

class GameObject {
  constructor(gameObjectSprite) {
    this.type = "game-object";
    this.sprite = gameObjectSprite;
    this.position = null;
    this.velocity = new Vector(0, 0);
    this.collisionBox = null;
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

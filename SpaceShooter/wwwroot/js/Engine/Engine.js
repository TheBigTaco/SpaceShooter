// Game is a static class
var Game = {
  gameObjects: {},
  lastObjectId: 0,
  textObjects: {},
  lastTextObjectId: 0,
  sprites: {},
  objectsToBeDisposed: [],
  currentScene: null,
  isRunning: false,
  startTime: null,
  tickNumber: 0,
  viewport: null,
  drawDebugInfo: false,
};
Game.initialize = function(canvas) {
  Game.viewport = new Viewport(canvas);
  Game.main();
}
Game.start = function() {
  Game.isRunning = true;
  Game.startTime = new Date().getTime();
}
Game.main = function() {
  var currentTickTime = new Date().getTime();
  var prevTickTime = currentTickTime;
  setInterval(function() {
    currentTickTime = new Date().getTime();
    Game.update(currentTickTime - prevTickTime);
    Game.checkCollisions();
    Game.deleteDisposedObjects();
    prevTickTime = currentTickTime;
  }, 10);
}
Game.update = function(dT) {
  if (Game.currentScene != null) {
    Game.currentScene.update();
  }
  if (Game.isRunning === true) {
    if (Object.keys(Game.gameObjects).length > 0) {
      Object.keys(Game.gameObjects).forEach(function(key) {
        Game.gameObjects[key].update();
        Game.gameObjects[key].physicsUpdate(dT);
      });
    }
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
Game.spawnText = function(textObject) {
  Game.lastTextObjectId++;
  textObject.id = Game.lastTextObjectId;
  Game.textObjects[textObject.id] = textObject;
}
Game.removeText = function(textObject) {
  delete Game.textObjects[textObject.id];
}
Game.markAsDisposed = function(gameObject) {
  gameObject.isDisposed = true;
  Game.objectsToBeDisposed.push(gameObject);
}
Game.deleteDisposedObjects = function() {
  Game.objectsToBeDisposed.forEach(function(gameObject) {
    delete Game.gameObjects[gameObject.id];
    Game.gameObjects;
  });
Game.objectsToBeDisposed = [];
}

class Scene {
  constructor(name) {
    this.name = name;
  }
  update() {

  }
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

      Object.keys(Game.textObjects).forEach(function(key) {
        // draw text
        var textObject = Game.textObjects[key];
        ctx.textAlign = textObject.alignment;
        ctx.textBaseline = textObject.baseline;
        ctx.fillStyle = textObject.fillStyle;
        ctx.font = textObject.font;
        ctx.fillText(textObject.text, textObject.position.x, textObject.position.y);
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
    this.collisionBox = null
    this.collidesWith = [];
    this.position = null;
    this.velocity = new Vector(0, 0);
  }
  update() {

  }
  physicsUpdate(dT) {
    if (this.position != null) {
      var deltaPostion = Vector.scale(dT / 1000, this.velocity);
      this.position = Vector.add(this.position, deltaPostion);
    }
  }
  despawn() {
    Game.markAsDisposed(this);
  }
  destroy() {
    Game.markAsDisposed(this);
  }
  getCollisionBoxPosition() {
    return (this.collisionBox != null) ? this.collisionBox.addOffsetVector(this.position) : null;
  }
  onCollision(collisionResult) {
    if (this.collidesWith.includes(collisionResult.collideTarget.type)) {
      this.doCollisionPhysics(collisionResult);
    }
  }
  doCollisionPhysics(collisionResult) {
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
}

class TextObject {
  constructor(text, position, textOptions) {
    this.text = text;
    this.position = position || new Vector(0, 0);
    this.textOptions = textOptions || {};
    this.font = this.textOptions.font || "20px sans-serif";
    this.fillStyle = this.textOptions.fillStyle || "rgb(0,0,0)";
    this.alignment = this.textOptions.alignment || "left";
    this.baseline = this.textOptions.baseline || "top";
  }
}

class GameObjectSprite {
  constructor(imgSrc, width, height, scale) {
    this.imgSrc = imgSrc;
    this.scale = (scale || 1);
    this.width = width * this.scale;
    this.height = height * this.scale;
    this.img = new Image();
    this.img.src = imgSrc;
  }
}

var Animation = {};
Animation.flash = function(gameObject, rate, time) {
  var numberOfFlashes = time / rate / 2;
  var sprite = gameObject.sprite;
  for (var i = 0; i < 2 * numberOfFlashes; i += 2) {
    setTimeout(function() {
      gameObject.sprite = null;
    }, i * rate);
    setTimeout(function() {
      gameObject.sprite = sprite;
    }, (i + 1) * rate);
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

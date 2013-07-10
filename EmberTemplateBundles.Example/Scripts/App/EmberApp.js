window.EmberApp = Ember.Application.create({
    LOG_TRANSITIONS: true,
    rootElement: $('#my-ember-app')
});

EmberApp.Router.map(function() {
    this.resource('about');
    this.resource('posts', function() {
        this.route('post', { path: ':post_id' });
    });
    
});

EmberApp.PostsRoute = Ember.Route.extend({
    model: function () {
        return EmberApp.Post.find();
    }
});


EmberApp.Post = Ember.Object.extend({});

EmberApp.Post.find = function (id) {
    if (id) {
        return EmberApp.Post.create({ id: id, name: "Post No " + id });
    }
    var theModel = [];
   
    for (var i = 1; i < 5; i++) {
        theModel.push(EmberApp.Post.create({ id: i, name: "Post No " + i }));
    }
    return theModel;
    
}
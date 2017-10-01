using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;
using System;
using System.Collections.Generic;


namespace MonoGame.Forms.Services
{
    public static class ServiceProvider
    {
        #region [ Initialize ]
        public static void Initialize(Game gameInstance)
        {
            _game = gameInstance;
            Graphics = _game.GraphicsDevice;
            services = new Dictionary<Type, object>();

            var mouseListener = new MouseListener(new MouseListenerSettings());
            var keyboardListener = new KeyboardListener(new KeyboardListenerSettings());
            _game.Components.Add(new InputListenerComponent(_game, mouseListener, keyboardListener));

            //AddService(new TextureMaker(Graphics));
            //AddService(new MouseCursor());
            _game.Components.Add(new GameViewport(_game));

            Initialized = true;
        }
        #endregion


        #region [ Members ]
        private static Game _game;
        private static InputListenerManager _inputManager;
        public static GraphicsDevice Graphics { get; private set; }
        private static Dictionary<Type, object> services;

        public static bool Initialized { get; private set; }
        #endregion


        #region [ Method: Update ]
        public static void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.Milliseconds;
            foreach (var service in services)
            {
                var serviceReference = service.Value as MonoGame.Extended.IUpdate;
                if (serviceReference != null)
                {
                    serviceReference.Update(gameTime);
                }
            }
        }
        #endregion


        #region [ Method: Draw ]
        public static void Draw(GameTime gameTime)
        {
            foreach (var service in services)
            {
                var serviceReference = service.Value as IDraw;
                if (serviceReference != null)
                {
                    serviceReference.Draw(gameTime);
                }
            }
        }
        #endregion


        #region [ Add / Remove Service ]
        public static void AddService(Type type, object provider)
        {
            if (!Initialized)
                throw new NotSupportedException("ServiceProvider not Initialized.");
            if (type == null)
                throw new ArgumentNullException("type");
            if (provider == null)
                throw new ArgumentNullException("provider");
            if (!IsAssignableFrom(type, provider))
                throw new ArgumentException("The provider does not match the specified service type!");

            services.Add(type, provider);
        }


        public static object GetService(Type type)
        {
            if (!Initialized)
                throw new NotSupportedException("ServiceProvider not Initialized.");
            if (type == null)
                throw new ArgumentNullException("type");

            object service;
            if (services.TryGetValue(type, out service))
                return service;

            return null;
        }


        public static void RemoveService(Type type)
        {
            if (!Initialized)
                throw new NotSupportedException("ServiceProvider not Initialized.");
            if (type == null)
                throw new ArgumentNullException("type");

            services.Remove(type);
        }


        public static T AddService<T>(T provider)
        {
            if (!Initialized)
                throw new NotSupportedException("ServiceProvider not Initialized.");
            AddService(typeof(T), provider);
            return provider;
        }


        public static T GetService<T>() where T : class
        {
            if (!Initialized)
                throw new NotSupportedException("ServiceProvider not Initialized.");

            var service = GetService(typeof(T));
            if (service == null)
                return null;

            return (T)service;
        }
        #endregion


        #region [ IsAssignableFrom ]
        /// <summary>
        /// Returns true if the given type can be assigned the given value
        /// </summary>
        public static bool IsAssignableFrom(Type type, object value)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (value == null)
                throw new ArgumentNullException("value");

            return IsAssignableFromType(type, value.GetType());
        }


        /// <summary>
        /// Returns true if the given type can be assigned a value with the given object type
        /// </summary>
        public static bool IsAssignableFromType(Type type, Type objectType)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (objectType == null)
                throw new ArgumentNullException("objectType");
            if (type.IsAssignableFrom(objectType))
                return true;
            return false;
        }
        #endregion

    }
}
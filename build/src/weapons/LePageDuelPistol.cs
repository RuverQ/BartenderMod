using DuckGame.BartenderMod.AmmoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.BartenderMod.Weapons
{
    [EditorGroup("BartenderMod|Guns")]
    public class LePageDuelPistol : Gun
    {
        private bool loaded = false;
        private bool loading = false;

        private EffectAnimation blink;

        int maxAmmo = 8;
        int minAmmo = 2;

        float reloadTime = 0.5f;

        float timesToSpin = 3f;

        private float loadProgress = 0;

        private SpriteMap _sprite;

        public float _angleOffset;

        public override float angle
        {
            get
            {
                return base.angle + _angleOffset;
            }
            set
            {
                _angle = value;
            }
        }

        public LePageDuelPistol(float xval, float yval) : base (xval, yval)
        {

            this.ammo = 3;
            this._ammoType = new AT12mm();
            wideBarrel = true;
            _barrelOffsetTL = new Vec2(18f, 4f);
            _type = "gun";

            center = new Vec2(6f, 7f);
            this.collisionSize = new Vec2(16f, 6f);
            this.collisionOffset = new Vec2(-4f, -4f);
            _kickForce = 4f;
            _fireSound = GetPath("Audio/SFX/LePageFire");
            graphic = new Sprite(GetPath("LePage"));

            _holdOffset = new Vec2(0f, 0f);
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.6f;

            _bio = "Fashion pistol from France";
            _editorName = "LePage The Duel Pistol";
            editorTooltip = "Puskin is dead.";
            physicsMaterial = PhysicsMaterial.Metal;

            _manualLoad = true;
        }

        public override void Initialize()
        {
            DevConsole.Log("Initialized");
            int loadedRand = Rando.Int(1);
            loaded = loadedRand == 0 ? loaded = true : loaded = false;
            this.ammo = Rando.Int(maxAmmo - minAmmo) + minAmmo + 1;
            base.Initialize();
        }

        private long currentMills()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        private void createEffectAnimation()
        {
            SpriteMap map = new SpriteMap(GetPath("Effects/blink"), 16, 16);
            map.AddAnimation("general", 1f, false, 0, 1, 2, 3, 4, 5, 6, 7);

            blink = new EffectAnimation(barrelPosition, map, 1f);
            Level.Add(blink);
            map.SetAnimation("general");
        }

        public override void Update()
        {
            base.Update();

            //Blink Allign
            if(blink != null && blink.active)
            {
                blink.position = barrelPosition;
            }

            //Load the gun
            if(loadProgress > 0)
            {
           
                loadProgress -= 1 / 60f;

                if (loadProgress <= 0)
                {
                    loading = false;
                    loaded = true;
                    Reload(shell: false);
                    PlaySFX(GetPath("Audio/SFX/LePageLoad"));
                    createEffectAnimation();
                }

                if (loading)
                {
                    _angleOffset += (timesToSpin / reloadTime) * (1f / 60f) * Maths.DegToRad(360);

                }else
                {
                    _angleOffset = 0;
                }
                
            }
        }

        public override void OnPressAction()
        {
            if (loaded)
            {
                base.OnPressAction();
                loaded = false;
            }
            else if(!loading)
            {
                loadProgress = reloadTime;
                PopShell();
                loading = true;
                PlaySFX(GetPath("Audio/SFX/LePageSpin"));
            }


            /*
            if (ammo > 0)
            {
                //_sprite.SetAnimation("fire");
                for (int i = 0; i < 3; i++)
                {
                    Vec2 vec = Offset(new Vec2(-9f, 0f));
                    Vec2 hitAngle = base.barrelVector.Rotate(Rando.Float(1f), Vec2.Zero);
                    //Level.Add(Spark.New(vec.x, vec.y, hitAngle, 0.1f));
                }
            }
            else
            {
                //_sprite.SetAnimation("empty");
            }
            */
        }



    }
}

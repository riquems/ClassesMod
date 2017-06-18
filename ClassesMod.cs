using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClassesMod
{
    public class ClassesModPlayer : ModPlayer
    {
        public override void OnEnterWorld(Player player)
        {
            Player Player = Main.player[Main.myPlayer];
            if (Player.statLifeMax == 100)
            {
                ClassUI.visible = true;
            }
        }
        public override void SetupStartInventory(IList<Item> items)
        {
            items.RemoveAt(2);
            items.RemoveAt(1);
            items.RemoveAt(0);
        }
    }
    public class Bow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bow");
            Tooltip.SetDefault("This is a modded sword.");
        }
        public override void SetDefaults()
        {
            item.damage = 50;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SparklingBall");
            item.shootSpeed = 16f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
                                                                                                                // If you want to randomize the speed to stagger the projectiles
                                                                                                                // float scale = 1f - (Main.rand.NextFloat() * .3f);
                                                                                                                // perturbedSpeed = perturbedSpeed * scale; 
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false; // return false because we don't want tmodloader to shoot projectile
        }
    }
    public class ClassUI : UIState
    {
        public UIPanel ChooseClass;
        public static bool visible = true;

        public override void OnInitialize()
        {
            ChooseClass = new UIPanel();
            ChooseClass.SetPadding(0);
            ChooseClass.Left.Set(400f, 0f);
            ChooseClass.Top.Set(100f, 0f);
            ChooseClass.Width.Set(800f, 0f);
            ChooseClass.Height.Set(300f, 0f);
            ChooseClass.BackgroundColor = new Color(73, 94, 171);

            ChooseClass.OnMouseDown += new UIElement.MouseEvent(DragStart);
            ChooseClass.OnMouseUp += new UIElement.MouseEvent(DragEnd);

            /*Texture2D buttonDeleteTexture = ModLoader.GetTexture("Terraria/UI/ButtonDelete");
            UIImageButton closeButton = new UIImageButton(buttonDeleteTexture);
            closeButton.Left.Set(740, 0f);
            closeButton.Top.Set(10, 0f);
            closeButton.Width.Set(22, 0f);
            closeButton.Height.Set(22, 0f);
            closeButton.OnClick += new MouseEvent(CloseButtonClicked);
            ChooseClass.Append(closeButton);*/

            UIText ChooseClassText = new UIText("Choose your class to start with:", 1, true);
            ChooseClassText.Left.Set(20, 0f);
            ChooseClassText.Top.Set(40, 0f);
            ChooseClassText.Width.Set(50, 0f);
            ChooseClassText.Height.Set(50, 0f);
            ChooseClass.Append(ChooseClassText);

            UIText WarriorText = new UIText("Warrior", 1, false);
            WarriorText.Left.Set(50, 0f);
            WarriorText.Top.Set(220, 0f);
            WarriorText.Width.Set(50, 0f);
            WarriorText.Height.Set(50, 0f);
            WarriorText.OnClick += new MouseEvent(WarriorButtonClicked);
            ChooseClass.Append(WarriorText);

            UIText RangerText = new UIText("Ranger", 1, false);
            RangerText.Left.Set(200, 0f);
            RangerText.Top.Set(220, 0f);
            RangerText.Width.Set(50, 0f);
            RangerText.Height.Set(50, 0f);
            RangerText.OnClick += new MouseEvent(RangerButtonClicked);
            ChooseClass.Append(RangerText);

            UIText SorcererText = new UIText("Sorcerer", 1, false);
            SorcererText.Left.Set(350, 0f);
            SorcererText.Top.Set(220, 0f);
            SorcererText.Width.Set(50, 0f);
            SorcererText.Height.Set(50, 0f);
            SorcererText.OnClick += new MouseEvent(SorcererButtonClicked);
            ChooseClass.Append(SorcererText);

            UIText ConjurerText = new UIText("Conjurer", 1, false);
            ConjurerText.Left.Set(500, 0f);
            ConjurerText.Top.Set(220, 0f);
            ConjurerText.Width.Set(50, 0f);
            ConjurerText.Height.Set(50, 0f);
            ConjurerText.OnClick += new MouseEvent(ConjurerButtonClicked);
            ChooseClass.Append(ConjurerText);

            UIText ThrowerText = new UIText("Thrower", 1, false);
            ThrowerText.Left.Set(650, 0f);
            ThrowerText.Top.Set(220, 0f);
            ThrowerText.Width.Set(50, 0f);
            ThrowerText.Height.Set(50, 0f);
            ThrowerText.OnClick += new MouseEvent(ThrowerButtonClicked);
            ChooseClass.Append(ThrowerText);

            Player Player = Main.player[Main.myPlayer];

            base.Append(ChooseClass);
        }

        public void WarriorButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Player Player = Main.player[Main.myPlayer];
            Player.statLifeMax -= 25;
            Player.PutItemInInventory(ItemID.WoodenSword);
            Player.PutItemInInventory(ItemID.CopperPickaxe);
            Player.PutItemInInventory(ItemID.CopperAxe);
            Main.PlaySound(SoundID.MenuOpen);

            visible = false;
        }

        public void RangerButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Player Player = Main.player[Main.myPlayer];
            Player.statLifeMax -= 50;
            Player.PutItemInInventory(ItemID.WoodenBow);
            Player.PutItemInInventory(ItemID.CopperPickaxe);
            Player.PutItemInInventory(ItemID.CopperAxe);
            Player.QuickSpawnItem(ItemID.WoodenArrow, 100);

            Main.PlaySound(SoundID.MenuOpen);
            visible = false;
        }

        private void SorcererButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Player Player = Main.player[Main.myPlayer];
            Player.statLifeMax -= 65;
            Player.PutItemInInventory(ItemID.WandofSparking);
            Player.PutItemInInventory(ItemID.CopperPickaxe);
            Player.PutItemInInventory(ItemID.CopperAxe);

            Main.PlaySound(SoundID.MenuOpen);
            visible = false;
        }

        private void ConjurerButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Player Player = Main.player[Main.myPlayer];
            Player.statLifeMax -= 65;
            Player.PutItemInInventory(ItemID.SlimeStaff);
            Player.PutItemInInventory(ItemID.CopperPickaxe);
            Player.PutItemInInventory(ItemID.CopperAxe);

            Main.PlaySound(SoundID.MenuOpen);
            visible = false;
        }

        private void ThrowerButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Player Player = Main.player[Main.myPlayer];
            Player.statLifeMax -= 50;
            Player.QuickSpawnItem(ItemID.Snowball, 50);
            Player.PutItemInInventory(ItemID.CopperPickaxe);
            Player.PutItemInInventory(ItemID.CopperAxe);

            Main.PlaySound(SoundID.MenuOpen);
            visible = false;
        }

        Vector2 offset;
        public bool dragging = false;

        private void DragStart(UIMouseEvent evt, UIElement listeningElement)
        {
            offset = new Vector2(evt.MousePosition.X - ChooseClass.Left.Pixels, evt.MousePosition.Y - ChooseClass.Top.Pixels);
            dragging = true;
        }

        private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
        {
            Vector2 end = evt.MousePosition;
            dragging = false;

            ChooseClass.Left.Set(end.X - offset.X, 0f);
            ChooseClass.Top.Set(end.Y - offset.Y, 0f);

            Recalculate();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Vector2 MousePosition = new Vector2((float)Main.mouseX, (float)Main.mouseY);
            if (ChooseClass.ContainsPoint(MousePosition))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
            if (dragging)
            {
                ChooseClass.Left.Set(MousePosition.X - offset.X, 0f);
                ChooseClass.Top.Set(MousePosition.Y - offset.Y, 0f);
                Recalculate();
            }
        }
    }
    public class ClassesMod : Mod
    {
        private UserInterface classUserInterface;
        internal ClassUI classUI;

        public ClassesMod()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }
        public override void Load()
        {
            if (!Main.dedServ)
            {
                classUI = new ClassUI();
                classUI.Activate();
                classUserInterface = new UserInterface();
                classUserInterface.SetState(classUI);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (MouseTextIndex != -1)
            {
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                    "ClassesMod: Choose Class",
                    delegate
                    {
                        if (ClassUI.visible)
                        {
                            classUserInterface.Update(Main._drawInterfaceGameTime);
                            classUI.Draw(Main.spriteBatch);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
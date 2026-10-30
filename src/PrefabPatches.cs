using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

// Both patches extend the game's Native GUI\Prefabs\GameMenu\GameMenuTroopSelection.xml.
namespace HideoutTroopCards
{
    /// <summary>Weapon icons in the bottom right corner of each card, left of the lock on your own card.</summary>
    [PrefabExtension("GameMenuTroopSelection", "descendant::GameMenuTroopSelectionItemButtonWidget/Children/ImageIdentifierWidget")]
    public sealed class TroopCardIconsPatch : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Append;

        [PrefabExtensionText]
        public string Content => @"
<ListPanel IsVisible=""@HtcHasWeaponIcons"" DoNotAcceptEvents=""true"" WidthSizePolicy=""CoverChildren"" HeightSizePolicy=""CoverChildren"" HorizontalAlignment=""Right"" VerticalAlignment=""Bottom"" MarginRight=""10"" MarginBottom=""10"" StackLayout.LayoutMethod=""HorizontalLeftToRight"">
  <Children>
    <ListPanel DataSource=""{HtcWeaponIcons}"" WidthSizePolicy=""CoverChildren"" HeightSizePolicy=""CoverChildren"" VerticalAlignment=""Bottom"" StackLayout.LayoutMethod=""HorizontalLeftToRight"">
      <ItemTemplate>
        <Widget WidthSizePolicy=""Fixed"" HeightSizePolicy=""Fixed"" SuggestedWidth=""20"" SuggestedHeight=""20"" MarginLeft=""2"">
          <Children>
            <DefaultLayerSpriteChangerBrushWidget DoNotAcceptEvents=""true"" WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""StretchToParent"" Brush=""GameMenu.ManageHideoutTroops.TierTypeSelectionBrush"" SpriteBrush=""HideoutTroopCards.WeaponIconSprites"" SpriteBrushLayerName=""@Text""/>
            <HintWidget DataSource=""{Hint}"" WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""StretchToParent"" Command.HoverBegin=""ExecuteBeginHint"" Command.HoverEnd=""ExecuteEndHint""/>
          </Children>
        </Widget>
      </ItemTemplate>
    </ListPanel>
    <Widget IsVisible=""@HtcIsPlayer"" DoNotAcceptEvents=""true"" WidthSizePolicy=""Fixed"" HeightSizePolicy=""Fixed"" SuggestedWidth=""36"" SuggestedHeight=""20""/>
  </Children>
</ListPanel>";
    }

    /// <summary>
    /// Sort dropdown and order arrow in the top right corner. A copy of the party screen's PartySortController prefab:
    /// its arrow sprite only loads with the party screen, so the arrow uses the always-loaded HideoutTroopCards.Sort brushes.
    /// </summary>
    [PrefabExtension("GameMenuTroopSelection", "descendant::ScrollablePanel[@InnerPanel='ClipRect\\TroopsGrid']")]
    public sealed class TroopSortPatch : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Append;

        [PrefabExtensionText]
        public string Content => @"
<Widget IsVisible=""@HtcShowSort"" WidthSizePolicy=""CoverChildren"" HeightSizePolicy=""CoverChildren"" HorizontalAlignment=""Right"" VerticalAlignment=""Top"" MarginTop=""50"" MarginRight=""35"">
  <Children>
    <ListPanel DataSource=""{HtcSortController}"" WidthSizePolicy=""CoverChildren"" HeightSizePolicy=""CoverChildren"" StackLayout.LayoutMethod=""HorizontalLeftToRight"">
      <Children>
        <AnimatedDropdownWidget DataSource=""{SortOptions}"" Id=""DropdownParent"" WidthSizePolicy=""Fixed"" HeightSizePolicy=""CoverChildren"" SuggestedWidth=""140"" HorizontalAlignment=""Center"" VerticalAlignment=""Center"" DropdownContainerWidget=""DropdownClipWidget\DropdownContainerWidget"" ListPanel=""DropdownClipWidget\DropdownContainerWidget\ScrollablePanel\ClipRect\PrimaryUsageSelectorList"" Button=""DropdownButtonContainer\DropdownButton"" CurrentSelectedIndex=""@SelectedIndex"" TextWidget=""DropdownButtonContainer\DropdownButton\SelectedTextWidget"" DropdownClipWidget=""DropdownClipWidget"" ScrollbarWidget=""DropdownClipWidget\DropdownContainerWidget\VerticalScrollbar"">
          <Children>
            <Widget Id=""DropdownButtonContainer"" WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""Fixed"" SuggestedHeight=""30"" HorizontalAlignment=""Center"" VerticalAlignment=""Center"">
              <Children>
                <ButtonWidget Id=""DropdownButton"" WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""StretchToParent"" DoNotPassEventsToChildren=""true"" Brush=""Quest.Sorting.Dropdown.Center.Small"">
                  <Children>
                    <ScrollingRichTextWidget Id=""SelectedTextWidget"" WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""StretchToParent"" HorizontalAlignment=""Center"" VerticalAlignment=""Center"" MarginLeft=""7"" MarginRight=""7"" PositionYOffset=""1"" Brush=""SPOptions.Dropdown.Center.Text"" IsAutoScrolling=""false"" ScrollOnHoverWidget=""..\..\DropdownButton""/>
                  </Children>
                </ButtonWidget>
              </Children>
            </Widget>
            <Widget Id=""DropdownClipWidget"" WidthSizePolicy=""CoverChildren"" HeightSizePolicy=""Fixed"" ClipContents=""true"" WidgetToCopyHeightFrom=""DropdownContainerWidget\ScrollablePanel"">
              <Children>
                <BrushWidget Id=""DropdownContainerWidget"" WidthSizePolicy=""Fixed"" HeightSizePolicy=""Fixed"" VerticalAlignment=""Bottom"" SuggestedWidth=""140"" SuggestedHeight=""380"" HorizontalAlignment=""Center"" Brush=""SPOptions.Dropdown.Extension"">
                  <Children>
                    <ScrollablePanel Id=""ScrollablePanel"" WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""CoverChildren"" AutoHideScrollBars=""true"" ClipRect=""ClipRect"" MarginBottom=""20"" VerticalAlignment=""Bottom"" InnerPanel=""ClipRect\PrimaryUsageSelectorList"" MaxHeight=""355"" VerticalScrollbar=""..\VerticalScrollbar"">
                      <Children>
                        <Widget Id=""ClipRect"" WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""CoverChildren"" ClipContents=""true"" MaxHeight=""355"">
                          <Children>
                            <ListPanel Id=""PrimaryUsageSelectorList"" DataSource=""{ItemList}"" WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""CoverChildren"" HorizontalAlignment=""Center"" StackLayout.LayoutMethod=""VerticalTopToBottom"">
                              <ItemTemplate>
                                <ButtonWidget Id=""DropdownItemButton"" DoNotUseCustomScale=""true"" DoNotPassEventsToChildren=""true"" WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""Fixed"" SuggestedHeight=""29"" MarginLeft=""10"" MarginRight=""10"" HorizontalAlignment=""Center"" VerticalAlignment=""Bottom"" ButtonType=""Radio"" UpdateChildrenStates=""true"" IsEnabled=""@CanBeSelected"" Brush=""Standard.DropdownItem.SoundBrush"">
                                  <Children>
                                    <ImageWidget WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""StretchToParent"" MarginLeft=""5"" MarginRight=""5"" Brush=""Standard.DropdownItem""/>
                                    <ScrollingRichTextWidget WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""StretchToParent"" HorizontalAlignment=""Center"" MarginLeft=""7"" MarginRight=""7"" VerticalAlignment=""Center"" Brush=""SPOptions.Dropdown.Item.Text"" Text=""@StringItem"" IsAutoScrolling=""false"" ScrollOnHoverWidget=""..\..\DropdownItemButton""/>
                                    <HintWidget DataSource=""{Hint}"" DoNotAcceptEvents=""true"" WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""StretchToParent"" Command.HoverBegin=""ExecuteBeginHint"" Command.HoverEnd=""ExecuteEndHint""/>
                                  </Children>
                                </ButtonWidget>
                              </ItemTemplate>
                            </ListPanel>
                          </Children>
                        </Widget>
                      </Children>
                    </ScrollablePanel>
                    <ScrollbarWidget Id=""VerticalScrollbar"" WidthSizePolicy=""Fixed"" HeightSizePolicy=""StretchToParent"" SuggestedWidth=""8"" HorizontalAlignment=""Right"" VerticalAlignment=""Center"" MarginTop=""15"" MarginBottom=""15"" AlignmentAxis=""Vertical"" Handle=""VerticalScrollbarHandle"" IsVisible=""false"" MaxValue=""100"" MinValue=""0"">
                      <Children>
                        <Widget WidthSizePolicy=""Fixed"" HeightSizePolicy=""StretchToParent"" SuggestedWidth=""4"" HorizontalAlignment=""Center"" Sprite=""BlankWhiteSquare_9"" AlphaFactor=""0.2"" Color=""#5A4033FF""/>
                        <ImageWidget Id=""VerticalScrollbarHandle"" WidthSizePolicy=""Fixed"" HeightSizePolicy=""Fixed"" SuggestedWidth=""8"" SuggestedHeight=""10"" HorizontalAlignment=""Center"" Brush=""FaceGen.Scrollbar.Handle""/>
                      </Children>
                    </ScrollbarWidget>
                  </Children>
                </BrushWidget>
              </Children>
            </Widget>
          </Children>
        </AnimatedDropdownWidget>
        <Widget WidthSizePolicy=""Fixed"" HeightSizePolicy=""Fixed"" SuggestedWidth=""16"" SuggestedHeight=""21"" MarginLeft=""12"" VerticalAlignment=""Center"">
          <Children>
            <BoolBrushChangerBrushWidget WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""StretchToParent"" IncludeChildren=""true"" BooleanCheck=""@IsAscending"" TrueBrush=""HideoutTroopCards.Sort.Ascending"" FalseBrush=""HideoutTroopCards.Sort.Descending"">
              <Children>
                <ButtonWidget WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""StretchToParent"" Command.Click=""ExecuteToggleOrder""/>
              </Children>
            </BoolBrushChangerBrushWidget>
          </Children>
        </Widget>
      </Children>
    </ListPanel>
  </Children>
</Widget>";
    }
}

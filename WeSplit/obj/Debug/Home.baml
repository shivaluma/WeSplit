   M S B A M L   `   `   `  ����B  >WeSplit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WeSplitWeSplit  5          WeSplit.Home   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:WeSplit     5      V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5           MaterialDesignBody#���   5	      $��Regular"�5
      $��13(�5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5       � 	Resources 5      3   -WeSplit.ImagePatchRelativeToAbsoluteConverter  imagePathConverter��%   &	         5   
   66   5      .��5      ?  9MaterialDesignThemes.Wpf.Transitions.TransitioningContent  -   5          Name$ TrainsitionigContentSlide��5   *   C  =MaterialDesignThemes.Wpf.Transitions.TransitioningContentBase   OpeningEffect 6M   D  >MaterialDesignThemes.Wpf.Transitions.TransitionEffectExtension  *
ExpandIn+   Duration$ 0:0:0X�.��5   
   ]� -   5        $��USHome��5      ��. .+�5      � $	��Top=�6   .+�5      � -   5        $	��Top��5      $��60��6'   $
��Left=�63   .+�5      �� -   5        $��TopImg��5      $��40��6*   $��40��66   $��10,0,5,0q�6A   $��Assets/map_120px.png��6S    �� BitmapScalingMode$ HighQuality=�6q   $
��Left=�6�   6�   �� 5      -   5        $��TopLabel��5      $��Các chuyến đi��6,   $��60��6D   $��24(�6P   $��Center=�6^   $
��Left=�6�   ���	#FF4848486�   5      � 5      $��60��6   $��Right=�6(   .+�5      �� -   5        $��SearchTexbox��5      $��	0 10 10 0q�5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tìm kiếm...��5      .  (MaterialDesignThemes.Wpf.TextFieldAssist   DecorationVisibility$ Hidden=�5      $��2q�5       $��5 0 0 0q�5!      $	��300��5"      $��40��5#      $��Center=�5$      	 �� 
CaretBrush	 �	#FF484848���#282828$��Center=�6f   $��16(�6�   
 �� SelectionBrush
 �	#FF215BFF6�   �� 5%      -   5        $��	imgSearch��5%      $��Assets/search_120px.png��6-   $��40��6N   $ HighQuality=�6u   $��UniformToFill=�6�   $��40��6�   $��	0 10 15 0q�6�   6�   5&      5'      �� 5(      -	   5        $	��Bot��5(   0   $y�	Collapsed=�6   $��0,0,30,0q�6;   $��50��6M   $��Bottom=�6Y    �� Orientation$ 
Horizontal=�6q   $��Center=�6�   $��Center=�6�   .+�5)      �� -
   5        $
��Prev��5.       & "MaterialDesignFloatingActionButton#��[ 5*      $��30��5+      $��30��5,      $��	0 0 100 0q�5-      ���	#FF34EDB5��50   ,   Z� .��51      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ ChevronLeftCircleOutline=�6.   $��30��6N   $��30��6Y   $��Center=�6e   $��Center=�6�   $��Center=�6�   $��Center=�6�   ���	#DDFFFFFF6�   52      �� 53      -   5        $
��Next��58      #��[ 54      $��30��55      $��30��56      $��0q�57      ���	#FF34EDB5��5:   ,   Z� .��5;        $ ChevronRightCircleOutline=�6.   $��30��6O   $��30��6Z   $��Center=�6f   $��Center=�6�   $��Center=�6�   $��Center=�6�   ���	#DDFFFFFF6�   5<      5=      � 5>      -   5        $
��Trip��5>      .+�5?      �� -   5        $��dataListview��5?      $��Hidden=�6/   
�������$��Disabled=�6t   $��Stretch=�6�   $��Stretch=�6  ��5@      �� 5A      .R�5C      A�  A� Columns$ 3��6*    A� Rows$ 1��66   $��5 0 5 0q�6?   6R   5D      ��5F      �� 5G      .R�5H      �� $��responsivetrip��6%   -   5         �� CornerRadius$ 15,15,15,15��5H   d   $��1q�6   ��5I   "   �� 5J   &   $ HighQuality=�6�    j� Stretch$ UniformToFill=�6�    �� ImageSource 61   �� *	Picture+ �� 	Converter# [ 6�   ._�5L   "   � .+�5M   &   �� $ 15,15,15,15��6-   ��5N   *   �� 5O   .    �� Color$ Black��6>    �� Opacity$	 0.5Q�6L   5Q   '   �� 5R   &   $	��205��61   $	��Top=�6>   $��	5,330,5,5q�6V   .+�5S   *   � $ HighQuality=�64   .+�5T   .     $
 Star=�6F   $��25��6R   $��25��6]   
��� ���6i     5U   .   $
 Star=�6F   $��25��6R   $��25��6]   
��� ���6i     5V   .   $
 Star=�6F   $��25��6R   $��25��6]   
��� ���6i     5W   .   $
 Star=�6F   $��25��6R   $��25��6]   
��� ���6i     5X   .   $
 Star=�6F   $��25��6R   $��25��6]   
��� ���6i   5Y   +   �� 5Z   *   $��16(�6I   
�������$��Center=�6j   $
��Bold"�6�   ��60   �� *Name+��6�   Z� 6�   �� 5[   *   $��5 0 5 0q�6O   $��14(�6`   
�������$
��Wrap=�6�   $��Center=�6�    �� TextAlignment$ Justify=�6�   ��64   �� *	Introduce+��6�   Z� 6�   5\   '   � 5]   &   $	��Top=�60   $��Right=�6H   $��10q�6d   .+�5^   *   �� $��40��61   $��40��6<   $ 40��6H   
�������._�5_   .   �� $��MapCheck��65   -   5         0 ,MaterialDesignFloatingActionMiniAccentButton#��[ 5_   \   $��Center=�6�   $��Center=�6�   $��Center=�6  $��Center=�61  $��0,0,0,0q�6N  ��6�   Z� ��6�   Z� .��5`   2     $��Check��6J   $ HighQuality=�6m   $��35��6�   $��35��6�   
��� � � 6W   �� *Icon+6�   5a   /   5b   +   5c   '   5d   #   5e      5j      5l      5m      5n      5o      5p      
   M S B A M L   `   `   `  ����  WpfApp1    WpfApp1.Pages.Sotrudnik   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5          N� Title$  	Sotrudnik��5       N� 
Background
 �����.��5      �  � RowDefinitions 5   
   �� 5      $
��199* �6   �� 5      $	��26* �6    � ColumnDefinitions 5   
   �� 5      $
��361* �6   �� 5      $
��439* �6   .+�5   
   -� -   5         -�Name$ flowDocementReader��5      $��0��69    -� ViewingMode$ Scroll=�6F   $��2��6[   $��0,0,0,0q�6o   .��5      /� .T�5      �� .r�5      �� -   5         ��Name$ employeesDataGrid��5      $��2��6:   $��Disabled=�6N   $��2��6�   ��5      �� 5      .R�5   "   �  K� IsItemsHost . ��6,   �� * (FrameworkElement.ActualWidth)+ �� RelativeSource �� 	 �� AncestorType$	 ScrollContentPresenterL�5        5!      ��5$      �� 5%      .R�5&   "   �� 
�������$��1q�6<   
 �� CornerRadius$
 5��6P   $��5q�6a    �� Padding$ 10q�6l   $	��200��6y   ._�5'   &   �� .+�5(   *   �� $��/Resources/OIP.jpg��60   $	��100��6L   $	��100��6X   $��UniformToFill=�5)   $   $��Center=�6<   $��0,0,0,10q�5*   $   �� 5+   *   $
��Bold"�6I   ��64   �� *Имя+6I   �� 5,   *   ��64   �� *Фамилия+�� 5-   *   ��64   �� *Отчество+�� 5.   *   ��64   �� *
nazvanie+�� 5/   *   ��64   �� *#!Контактные_данные+50   '   51   #   52      56      58      59      5;      �� 5<   
   -   5         ��Name$ PrintListButton��5<      $��Печать списка��6*   $��1��6B   $��141,9,178,3q�6R   $��1��6�   �� 5=   
   -   5        $ adduser��5=      $��Добавить��6"   $��
289,8,30,4q�65   $��1��6I   $��1��6o   5A      5E      
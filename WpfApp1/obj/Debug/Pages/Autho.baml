   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfApp1.PagesWpfApp1.Pages  5          WpfApp1.Pages.Autho   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:WpfApp1.Pages     5        N� Title$  Autho��5	       N� 
Background
 �����.��5      �  � ColumnDefinitions 5   
   �� 5      $	��260 �6   �� 5      �� 5      $	��260 �6   .+�5   
   �� $��1��6   $��1��6"   $��Center=�62   $��Center=�6O   $	��448��6j   .+�5      �� $��
Логин��6   �� 5      -   5         ��Name$ tbLogin��5      �� 5      $��Пароль��6   �� 5      -   5        $ 
tbPassword��5      �� 5      -   5         ��Name$ 
tblCaptcha��5      $��
Капча��6,   $y�Hidden=�69   �� 5      -   5        $ 
tbTimeLeft��5      $	��200��6,   
���  ��$y�Visible=�6I   �� 5      -   5        $ tbTimer��5      �� 5      -   5        $ 	tbCaptcha��5      $y�Hidden=�6)   �� 5      -   5         ��Name$ 	labelcode��5      $y�Hidden=�6'   $��Код��6;   �� 5      -   5        $ tbcode��5      $y�Hidden=�6&   �� 5        �� Orientation$ 
Horizontal=�6   $��80��62   $��Center=�6>   .+�5!      �� -	   5         ��Name$ btnEnterGuest��5!      $��5,15,5,5q�60   $"��Войти как гость��6B    � Cursor$
 Hand��6x   �� 5"      -
   5        $ btnEnter��5"      $��5,15,5,5q�6+   $��
Войти��6=   6M   5&      �� 5'      $ 
Horizontal=�6   .+�5(      �� -   5        $ 
bntconfirm��5(      $y�Hidden=�6)   $��Потдвердить��6=   6S   �� 5)      -   5        $ Forgotpassword��5)   *   $y�Hidden=�6   $��Забыл пароль��6B   	 J� Opacity$	 1Q�6v   
�������
�������
 J� RenderTransformOrigin$
 
1.35,0.398&�6�   5*      5+      5/      51      
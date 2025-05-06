using UnityEngine;

public static class Utils
{
    public const float TOUCH_SENSITIVITY = 5f;
    public const float MOUSE_SENSITIVITY = 500f;

    public const float HORIZONTAL_MOVEMENT_SPEED_VALUE = 10f;
    public const float FORWARD_MOVEMENT_SPEED_VALUE = 10f;

    public const float HORIZONTAL_LIMIT_VALUE = 4;
    public static Vector3 PLAYER_START_POSITION = new Vector3(0, 0, -22);

    public const int COLUMN_COUNT = 4;
    public const float BLOCK_HORIZONTAL_SPACE_SIZE = 2.3f;

    public static Vector3 BLOCK_VERTICAL_SPACE_SIZE = new Vector3(0, 0.1f, 0);

    public static Vector3 STACK_BASE_SCALE = new Vector3(1.5f, 0.1f, 1);
    public static Vector3 CHARGED_STACK_BASE_SCALE = new Vector3(8f, 0.1f, 1);

    public const int CHARGE_LEVEL_LIMIT = 30;

    public const string KICK_ANIMATION_TRIGGER_NAME = "Kick";
    public const string RUN_ANIMATION_NAME = "Run";
    public const float COIN_ANIMATION_DURATION = 0.5f;

    public const float POP_UP_ANIMATION_DURATION = 0.5f;

    public const string PLAYER_DATA_FILE_NAME = "/PlayerData.json";


}

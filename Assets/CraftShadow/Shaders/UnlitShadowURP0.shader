Shader "Custom/UnlitShadowURP0" 
{
	Properties
	{
		_StencilRef("Stencil Ref", Int) = 4
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent-1"
			"IgnoreProjector" = "True"
		}
		LOD 100

		Pass
		{
			ColorMask 0

			ZWrite Off
			ZTest LEqual
			Cull Front
			Blend Off

            Stencil
            {
                Ref [_StencilRef]
                Comp Greater
                ZFail Replace
            }
		}
	}
}
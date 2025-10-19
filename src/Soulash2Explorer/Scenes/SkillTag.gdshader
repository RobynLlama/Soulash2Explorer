shader_type canvas_item;

uniform vec4 background_color: source_color;
uniform vec4 progress_color: source_color;
uniform float progress;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	if (UV.r > progress / 100.0) {
		COLOR = background_color;
	}
	else {
		COLOR = progress_color;
	}
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}

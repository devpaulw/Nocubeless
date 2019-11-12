#version 420 core
out vec4 color;
  
uniform vec4 cubecolor;
uniform vec4 lightcolor;

void main()
{
    color = lightcolor * cubecolor;
}
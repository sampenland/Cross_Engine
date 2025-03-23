function getFPS()

	return c_game.FPS

end

function log(text)

	c_console:Print(text)

end

function createView(x, y, width, height)

	local currentScene = c_scene_manager.CurrentScene
	if currentScene ~= nil then
		currentScene:LuaCreateView(x, y, width, height)
	end

end

function worldtext(x, y, text, size, r, g, b, a)

	local currentScene = c_scene_manager.CurrentScene
	if currentScene ~= nil then
		currentScene:LuaCreateWorldText(x, y, text, size, r, g, b, a)
	end

end
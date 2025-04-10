function getFPS()

	return c_game.FPS

end
--================================================================================
-- log(text)     Prints output into scene's console (and to Cross Log)
--------
function log(text)

	c_console:Print(text)

end
--================================================================================

--================================================================================
-- createview(x, y, width, height)              Creates another view (think mini-map)
------------
function createView(x, y, width, height)

	local currentScene = c_scene_manager.CurrentScene
	if currentScene ~= nil then
		currentScene:LuaCreateView(x, y, width, height)
	end

end
--================================================================================

--================================================================================
-- worldtext(x, y, text, size, r, g, b, a)
--
-- RGB ex: 255, 255, 255, 255 = white full transparent
-- WORLD TEXT FUCTIONS:
--			:setText(text)		Sets text that is displayed
--			:setPos(x , y)		Sets position of text

function worldtext(x, y, text, size, r, g, b, a)

	local currentScene = c_scene_manager.CurrentScene
	if currentScene ~= nil then
		return currentScene:LuaCreateWorldText(x, y, text, size, r, g, b, a)
	end

end
--================================================================================